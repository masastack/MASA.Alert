// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

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
    public async Task QueryLogAggregationAsync(CheckAlarmRuleCommand command)
    {
        var alarmRule = command.AlarmRule;
        var checkTime = DateTime.Now;
        var latest = alarmRule.GetLatest();
        command.ConsecutiveCount = latest?.ConsecutiveCount ?? 0;
        var startTime = alarmRule.GetStartCheckTime(checkTime, latest);

        if (startTime == null)
        {
            alarmRule.SkipCheck();
            command.IsStop = true;
            return;
        }

        Random a = new Random();

        foreach (var item in alarmRule.LogMonitorItems)
        {
            var fieldMaps = new List<FieldAggregationRequest>
            {
                new FieldAggregationRequest
                {
                    Name = item.Field,
                    Alias = item.Alias,
                    AggregationType = (AggregationTypes)item.AggregationType
                }
            };

            var request = new LogAggregationRequest
            {
                FieldMaps = fieldMaps,
                Query = alarmRule.WhereExpression,
                Start = startTime.Value,
                End = checkTime,
            };

            var result = await _tscClient.LogService.GetAggregationAsync(request);
            command.AggregateResult.TryAdd(item.Alias, a.Next(100));
        }
    }

    [EventHandler(3)]
    public async Task ExecuteRulesAsync(CheckAlarmRuleCommand command)
    {
        if (command.IsStop) return;

        await _domainService.CheckRuleAsync(command.AlarmRule, command.AggregateResult);
    }
}
