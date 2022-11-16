// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Services;

public class AlarmRuleDomainService : DomainService
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IRulesEngineClient _rulesEngineClient;

    public AlarmRuleDomainService(IDomainEventBus eventBus
        , IAlarmRuleRepository repository
        , IRulesEngineClient rulesEngineClient) : base(eventBus)
    {
        _repository = repository;
        _rulesEngineClient = rulesEngineClient;
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

    public async Task CheckRuleAsync(AlarmRule alarmRule, ConcurrentDictionary<string, long> aggregateResult)
    {
        var triggerRuleItems = new List<AlarmRuleItem>();

        foreach (var item in alarmRule.Items)
        {
            var result = await _rulesEngineClient.ExecuteAsync(item.Expression, aggregateResult);

            if (result[0].IsValid)
            {
                triggerRuleItems.Add(item);
            }
        }

        alarmRule.Check(aggregateResult, triggerRuleItems);
        await _repository.UpdateAsync(alarmRule);
    }

    public DateTime? GetStartCheckTime(AlarmRule alarmRule, DateTime checkTime, AlarmRuleRecord? latest)
    {
        if (alarmRule.CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron && latest == null)
        {
            return null;
        }

        if (alarmRule.CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron)
        {
            return latest?.CreationTime;
        }

        if (alarmRule.CheckFrequency.Type == AlarmCheckFrequencyTypes.FixedInterval)
        {
            return alarmRule.GetStartTime(checkTime);
        }

        return null;
    }
}
