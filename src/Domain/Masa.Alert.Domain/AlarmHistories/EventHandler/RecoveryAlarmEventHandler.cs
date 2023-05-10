// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.EventHandler;

public class RecoveryAlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IEventBus _eventBus;

    public RecoveryAlarmEventHandler(IAlarmHistoryRepository repository
        , IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task HandleEventAsync(RecoveryAlarmEvent eto)
    {
        var alarm = await _repository.GetLastAsync(eto.AlarmRuleId);

        if (alarm == null) return;

        alarm.Recovery(true);
        alarm.AddAlarmRuleRecord(eto.ExcuteTime, eto.AggregateResult, eto.IsTrigger, eto.ConsecutiveCount, eto.TriggerRuleItems);
        await _repository.UpdateAsync(alarm);
    }
}
