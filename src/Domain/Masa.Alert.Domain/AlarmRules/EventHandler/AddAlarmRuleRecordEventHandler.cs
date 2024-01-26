// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.EventHandler;

public class AddAlarmRuleRecordEventHandler
{
    private readonly IAlarmRuleRecordRepository _repository;

    public AddAlarmRuleRecordEventHandler(IAlarmRuleRecordRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(AddAlarmRuleRecordEvent eto)
    {
        var alarm = new AlarmRuleRecord(eto.AlarmRuleId, eto.AggregateResult, eto.IsTrigger, eto.ConsecutiveCount, eto.ExcuteTime, eto.RuleResultItems, eto.AlarmHistoryId);

        await _repository.AddAsync(alarm);
    }
}
