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

    public async Task CheckRuleAsync(DateTimeOffset excuteTime, AlarmRule alarmRule, ConcurrentDictionary<string, long> aggregateResult)
    {
        var ruleResult = new List<RuleResultItem>();

        foreach (var item in alarmRule.Items)
        {
            var result = await _rulesEngineClient.ExecuteAsync(item.Expression, aggregateResult);
            var ruleResultItem = new RuleResultItem(result[0].IsValid, item);
            ruleResult.Add(ruleResultItem);
        }

        alarmRule.Check(excuteTime, aggregateResult, ruleResult);
        await _repository.UpdateAsync(alarmRule);
    }
}
