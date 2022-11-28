// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.EventHandler;

public class UpdateAlarmRuleRecordAlarmIdEventHandler
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IEventBus _eventBus;

    public UpdateAlarmRuleRecordAlarmIdEventHandler(IAlarmRuleRepository repository
        , IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task HandleEventAsync(UpdateAlarmRuleRecordAlarmIdEvent eto)
    {
        var alarm = await _repository.FindAsync(x=>x.Id==eto.AlarmRuleId);
        if (alarm == null) return;

        var latest = alarm.GetLatest();
        if (latest == null) return;

        latest.SetAlarmHistoryId(eto.AlarmHistoryId);
        await _repository.UpdateAsync(alarm);
    }
}
