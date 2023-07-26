// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Services;

public class AlarmRuleDomainService : DomainService
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IRulesEngineClient _rulesEngineClient;
    private readonly IDistributedCacheClient _cacheClient;

    public AlarmRuleDomainService(IDomainEventBus eventBus
        , IAlarmRuleRepository repository
        , IRulesEngineClient rulesEngineClient
        , IDistributedCacheClient cacheClient) : base(eventBus)
    {
        _repository = repository;
        _rulesEngineClient = rulesEngineClient;
        _cacheClient = cacheClient;
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

        var latestRecord = alarmRule.GetLatest();

        var cacheKey = $"{AlarmCacheKeys.ALARM_CONSECUTIVE_COUNT}_{alarmRule.Id}";
        var consecutiveCount = Convert.ToInt32(await _cacheClient.HashIncrementAsync(cacheKey));

        var isRuleValid = alarmRule.IsRuleValid(ruleResult);
        var isTrigger = isRuleValid && consecutiveCount >= alarmRule.ContinuousTriggerThreshold;

        if (isTrigger)
        {
            alarmRule.TriggerAlarm(excuteTime, aggregateResult, ruleResult, consecutiveCount);
        }
        else if (latestRecord != null && latestRecord.IsTrigger)
        {
            alarmRule.RecoveryAlarm(excuteTime, aggregateResult, ruleResult);
        }
        else
        {
            if (!isRuleValid)
            {
                consecutiveCount = 0;
                await _cacheClient.RemoveAsync<long>(cacheKey);
            }
            
            alarmRule.AddAggregateResult(excuteTime, aggregateResult, false, consecutiveCount, ruleResult);
        }

        await _repository.UpdateAsync(alarmRule);
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
