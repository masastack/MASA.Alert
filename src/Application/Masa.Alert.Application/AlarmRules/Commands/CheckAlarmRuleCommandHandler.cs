// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Commands;

public class CheckAlarmRuleCommandHandler
{
    private readonly AlarmRuleDomainService _domainService;
    private readonly IAlarmRuleRepository _repository;
    private readonly ITscClient _tscClient;
    private readonly IRulesEngineClient _rulesEngineClient;
    private readonly ILogger<CheckAlarmRuleCommandHandler> _logger;
    private readonly II18n<DefaultResource> _i18n;

    public CheckAlarmRuleCommandHandler(AlarmRuleDomainService domainService
        , IAlarmRuleRepository repository
        , ITscClient tscClient
        , IRulesEngineClient rulesEngineClient
        , ILogger<CheckAlarmRuleCommandHandler> logger
        , II18n<DefaultResource> i18n)
    {
        _domainService = domainService;
        _repository = repository;
        _tscClient = tscClient;
        _rulesEngineClient = rulesEngineClient;
        _logger = logger;
        _i18n = i18n;
    }

    [EventHandler(1)]
    public async Task QueryRulesAsync(CheckAlarmRuleCommand command)
    {
        var queryable = await _repository.WithDetailsAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id == command.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));

        if (!entity.IsEnabled)
            throw new UserFriendlyException(errorCode: UserFriendlyExceptionCodes.ALARM_RULE_DISABLE);

        command.AlarmRule = entity;
    }

    [EventHandler(2)]
    public async Task QueryAggregationAsync(CheckAlarmRuleCommand command)
    {
        var alarmRule = command.AlarmRule;
        var checkTime = command.ExcuteTime ?? DateTimeOffset.Now;
        var latest = alarmRule.GetLatest();
        var startTime = alarmRule.GetStartCheckTime(checkTime, latest);

        if (startTime == null)
        {
            alarmRule.SkipCheck(checkTime);
            command.IsStop = true;
            return;
        }

        if (alarmRule.Type == AlarmRuleTypes.Log)
        {
            command.AggregateResult = await QueryLogAggregationAsync(alarmRule, startTime.Value.UtcDateTime, checkTime.UtcDateTime, command);
        }

        if (alarmRule.Type == AlarmRuleTypes.Metric)
        {
            command.AggregateResult = await QueryMetricAggregationAsync(alarmRule, startTime.Value.UtcDateTime, checkTime.UtcDateTime, command);
        }

        if (!command.AggregateResult.Any())
        {
            command.IsStop = true;
        }
    }

    [EventHandler(3)]
    public async Task ExecuteRulesAsync(CheckAlarmRuleCommand command)
    {
        if (command.IsStop)
        {
            command.AlarmRule.AddAggregateResult(command.ExcuteTime ?? DateTimeOffset.Now, command.AggregateResult, false, 0, new List<RuleResultItem>());
            await _repository.UpdateAsync(command.AlarmRule);
            return;
        }

        await _domainService.CheckRuleAsync(command.ExcuteTime ?? DateTimeOffset.Now, command.AlarmRule, command.AggregateResult);
    }

    private async Task<ConcurrentDictionary<string, long>> QueryLogAggregationAsync(AlarmRule alarmRule, DateTime startTime, DateTime endTime, CheckAlarmRuleCommand command)
    {
        var aggregateResult = new ConcurrentDictionary<string, long>();

        foreach (var item in alarmRule.LogMonitorItems)
        {
            if (item.IsOffset && item.OffsetPeriod > 0)
            {
                var offsetResult = alarmRule.GetOffsetResult(item.OffsetPeriod, item.Alias);
                if (offsetResult.HasValue)
                {
                    aggregateResult.TryAdd(item.Alias, offsetResult.Value);
                    return aggregateResult;
                }
                else
                {
                    command.IsStop = true;
                    _logger.LogInformation("The offset data has not been generated");
                }
            }

            var request = new SimpleAggregateRequestDto
            {
                Service = alarmRule.AppIdentity,
                Name = item.Field,
                Alias = item.Alias,
                Type = (AggregateTypes)item.AggregationType.Id,
                RawQuery = alarmRule.WhereExpression,
                Start = startTime,
                End = endTime,
            };

            var result = await _tscClient.LogService.GetAggregationAsync<long>(request);
            aggregateResult.TryAdd(item.Alias, result);
        }

        return aggregateResult;
    }

    private async Task<ConcurrentDictionary<string, long>> QueryMetricAggregationAsync(AlarmRule alarmRule, DateTime startTime, DateTime endTime, CheckAlarmRuleCommand command)
    {
        var aggregateResult = new ConcurrentDictionary<string, long>();

        foreach (var item in alarmRule.MetricMonitorItems)
        {
            if (item.IsOffset && item.OffsetPeriod > 0)
            {
                var offsetResult = alarmRule.GetOffsetResult(item.OffsetPeriod, item.Alias);
                if (offsetResult.HasValue)
                {
                    aggregateResult.TryAdd(item.Alias, offsetResult.Value);
                    return aggregateResult;
                }
                else
                {
                    command.IsStop = true;
                    _logger.LogInformation("The offset data has not been generated");
                }
            }

            var req = new ValuesRequest
            {
                Match = item.GetExpression(),
                Start = startTime,
                End = endTime,
            };

            var result = await _tscClient.MetricService.GetValuesAsync(req);
            aggregateResult.TryAdd(item.Alias, Convert.ToInt64(result));
        }

        return aggregateResult;
    }
}
