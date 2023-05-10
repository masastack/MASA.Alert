// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Domain.AlarmHistories.Aggregates;
using Masa.Alert.Domain.AlarmRules.Aggregates;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;

namespace Masa.Alert.Domain.AlarmRules.Services;

public class AlarmRuleDomainService : DomainService
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IAlarmHistoryRepository _alarmHistoryRepository;
    private readonly IRulesEngineClient _rulesEngineClient;

    public AlarmRuleDomainService(IDomainEventBus eventBus
        , IAlarmRuleRepository repository
        , IAlarmHistoryRepository alarmHistoryRepository
        , IRulesEngineClient rulesEngineClient) : base(eventBus)
    {
        _repository = repository;
        _rulesEngineClient = rulesEngineClient;
        _alarmHistoryRepository = alarmHistoryRepository;
    }

    public async Task ValidateRuleAsync(AlarmRule alarmRule)
    {
        foreach (var item in alarmRule.Items)
        {
            var result = _rulesEngineClient.Verify(item.Expression);

            if (!result.IsValid)
            {
                throw new UserFriendlyException(result.Message);
            }
        }
        await Task.CompletedTask;
    }

    public async Task CheckRuleAsync(DateTimeOffset excuteTime, AlarmRule alarmRule, ConcurrentDictionary<string, long> aggregateResult)
    {
        var ruleResult = new List<RuleResultItem>();

        foreach (var item in alarmRule.Items)
        {
            var result = await _rulesEngineClient.ExecuteAsync(item.Expression, aggregateResult);
            var ruleResultItem = new RuleResultItem(result[0].IsValid, item);
            ruleResult.Add(ruleResultItem);
        }

        //alarmRule.Check(excuteTime, aggregateResult, ruleResult);

        var latestRecord = alarmRule.GetLatest();
        var consecutiveCount = latestRecord?.ConsecutiveCount ?? 0;

        var isTrigger = alarmRule.CheckIsTrigger(ruleResult, consecutiveCount);
        var alarm = await _alarmHistoryRepository.GetLastAsync(alarmRule.Id);

        if (isTrigger && alarm != null && !alarm.RecoveryTime.HasValue)
        {
            consecutiveCount++;
        }
        else
        {
            consecutiveCount = 0;
        }

        if (isTrigger)
        {
            alarm = await HandleAlarmAsync(alarmRule, ruleResult, alarm);
        }
        else if (latestRecord != null && latestRecord.IsTrigger && alarm != null)
        {
            await HandleAlarmRecoveryAsync(alarm);
        }

        alarmRule.AddAggregateResult(excuteTime, aggregateResult, isTrigger, consecutiveCount, ruleResult, alarm?.Id ?? default);
        await _repository.UpdateAsync(alarmRule);
    }

    public async Task<AlarmHistory?> HandleAlarmAsync(AlarmRule alarmRule, List<RuleResultItem> ruleResult, AlarmHistory? alarm)
    {
        var alertSeverity = alarmRule.GetAlertSeverity(ruleResult);

        var isNotification = alarmRule.CheckIsNotification();
        var isSilence = alarmRule.CheckIsSilence(alarm?.LastNotificationTime);

        if (alarm == null || alarm.RecoveryTime.HasValue)
        {
            alarm = new AlarmHistory(alarmRule.Id, alertSeverity, ruleResult);
            alarm.SetIsNotification(isNotification, isSilence);
            await _alarmHistoryRepository.AddAsync(alarm);
        }
        else
        {
            alarm.Update(alertSeverity, isNotification, ruleResult);
            alarm.SetIsNotification(isNotification, isSilence);
            await _alarmHistoryRepository.UpdateAsync(alarm);
        }

        return alarm;
    }

    public async Task HandleAlarmRecoveryAsync(AlarmHistory alarm)
    {
        alarm.Recovery(true);
        await _alarmHistoryRepository.UpdateAsync(alarm);
    }

    public async Task CreateAsync(AlarmRule alarmRule, bool isEnabled, CheckFrequency checkFrequency)
    {
        await ValidateRuleAsync(alarmRule);

        alarmRule.CheckJob(isEnabled, checkFrequency);

        await _repository.AddAsync(alarmRule);
    }

    public async Task UpdateAsync(AlarmRule alarmRule, bool isEnabled, CheckFrequency checkFrequency)
    {
        await ValidateRuleAsync(alarmRule);

        alarmRule.CheckJob(isEnabled, checkFrequency);

        await _repository.UpdateAsync(alarmRule);
    }
}
