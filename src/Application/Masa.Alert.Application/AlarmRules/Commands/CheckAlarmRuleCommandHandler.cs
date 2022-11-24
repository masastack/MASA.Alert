// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using System.Collections.Generic;

namespace Masa.Alert.Application.AlarmRules.Commands;

public class CheckAlarmRuleCommandHandler
{
    private readonly AlarmRuleDomainService _domainService;
    private readonly IAlarmRuleRepository _repository;
    private readonly ITscClient _tscClient;
    private readonly IRulesEngineClient _rulesEngineClient;

    public CheckAlarmRuleCommandHandler(AlarmRuleDomainService domainService
        , IAlarmRuleRepository repository
        , ITscClient tscClient
        , IRulesEngineClient rulesEngineClient)
    {
        _domainService = domainService;
        _repository = repository;
        _tscClient = tscClient;
        _rulesEngineClient = rulesEngineClient;
    }

    [EventHandler(1)]
    public async Task QueryRulesAsync(CheckAlarmRuleCommand command)
    {
        var queryable = await _repository.WithDetailsAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id == command.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        command.AlarmRule = entity;
    }

    [EventHandler(2)]
    public async Task QueryAggregationAsync(CheckAlarmRuleCommand command)
    {
        var alarmRule = command.AlarmRule;
        var checkTime = DateTime.Now;
        var latest = alarmRule.GetLatest();
        var startTime = alarmRule.GetStartCheckTime(checkTime, latest);

        if (startTime == null)
        {
            alarmRule.SkipCheck();
            command.IsStop = true;
            return;
        }

        if (alarmRule.AlarmRuleType == AlarmRuleTypes.Log)
        {
            command.AggregateResult = await QueryLogAggregationAsync(alarmRule, startTime.Value, checkTime);
        }

        if (alarmRule.AlarmRuleType == AlarmRuleTypes.Metric)
        {
            command.AggregateResult = await QueryMetricAggregationAsync(alarmRule, startTime.Value, checkTime);
        }
    }

    [EventHandler(3)]
    public async Task ExecuteRulesAsync(CheckAlarmRuleCommand command)
    {
        if (command.IsStop) return;

        await _domainService.CheckRuleAsync(command.AlarmRule, command.AggregateResult);
    }

    private async Task<ConcurrentDictionary<string, long>> QueryLogAggregationAsync(AlarmRule alarmRule, DateTime startTime, DateTime endTime)
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
                    continue;
                }
            }

            var request = new SimpleAggregateRequestDto
            {
                Service = alarmRule.AppIdentity,
                Name = item.Field,
                Alias = item.Alias,
                Type = (AggregateTypes)item.AggregationType,
                RawQuery = alarmRule.WhereExpression,
                Start = startTime,
                End = endTime,
            };

            var result = await _tscClient.LogService.GetAggregationAsync<long>(request);
            aggregateResult.TryAdd(item.Alias, result);
        }

        return aggregateResult;
    }

    private async Task<ConcurrentDictionary<string, long>> QueryMetricAggregationAsync(AlarmRule alarmRule, DateTime startTime, DateTime endTime)
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
                    continue;
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
