// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

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
}
