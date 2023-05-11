// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.EventHandler;

public class AddAlarmRuleRecordEventHandler
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IEventBus _eventBus;

    public AddAlarmRuleRecordEventHandler(IAlarmRuleRepository repository
        , IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task HandleEventAsync(AddAlarmRuleRecordEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarm == null) return;

        alarm.AddAggregateResult(eto.ExcuteTime, eto.AggregateResult, eto.IsTrigger, eto.ConsecutiveCount, eto.RuleResultItems, eto.AlarmHistoryId);

        await _repository.UpdateAsync(alarm);
    }
}
