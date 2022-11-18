// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Domain.AlarmRules.Aggregates;

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
        var ruleResult = new List<RuleResultItem>();

        foreach (var item in alarmRule.Items)
        {
            var ruleResultItem = new RuleResultItem(item.Expression,item.AlertSeverity,item.IsRecoveryNotification,item.IsNotification, item.RecoveryNotificationConfig, item.NotificationConfig);
            var result = await _rulesEngineClient.ExecuteAsync(item.Expression, aggregateResult);
            ruleResultItem.IsValid = result[0].IsValid;
            ruleResult.Add(ruleResultItem);
        }

        alarmRule.Check(aggregateResult, ruleResult);
        await _repository.UpdateAsync(alarmRule);
    }
}
