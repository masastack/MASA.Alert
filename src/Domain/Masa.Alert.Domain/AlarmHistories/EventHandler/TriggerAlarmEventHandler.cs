// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.EventHandler;

public class TriggerAlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRulerepository;
    private readonly IEventBus _eventBus;

    public TriggerAlarmEventHandler(IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRulerepository
        , IEventBus eventBus)
    {
        _repository = repository;
        _alarmRulerepository = alarmRulerepository;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task HandleEventAsync(TriggerAlarmEvent eto)
    {
        var alarmRule = await _alarmRulerepository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule == null) return;

        var alarm = await _repository.GetLastAsync(eto.AlarmRuleId);
        var isNotification = alarmRule.CheckIsNotification(alarm?.LastNotificationTime);

        if (alarm == null || alarm.RecoveryTime.HasValue)
        {
            alarm = new AlarmHistory(eto.AlarmRuleId, eto.AlertSeverity, isNotification, eto.TriggerRuleItems);
            await _repository.AddAsync(alarm);
        }
        else
        {
            alarm.Update(eto.AlertSeverity, isNotification, eto.TriggerRuleItems);
            alarm.SetIsNotification(isNotification);
            await _repository.UpdateAsync(alarm);
        }

        if (alarm.IsNotification)
        {
            await _eventBus.PublishAsync(new SendAlarmNotificationEvent(alarm.Id));
        }
    }
}
