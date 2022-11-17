// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.EventHandler;

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
        var alarm = await _repository.GetLastAsync(eto.AlarmRuleId);

        if (alarm == null)
        {
            alarm = new AlarmHistory(eto.AlarmRuleId, eto.AlertSeverity, eto.AlarmRuleItems);
            await _repository.AddAsync(alarm);
        }
        else
        {
            alarm.TriggerAlarm();
            await _repository.UpdateAsync(alarm);
        }
    }

    private async Task HandleNotification(AlarmHistory alarm)
    {
        var alarmRule = await _alarmRulerepository.FindAsync(x => x.Id == alarm.Id);
        if (alarmRule != null)
        {
            if (alarm.LastNotificationTime == null)
            {
                await _eventBus.PublishAsync(new SendAlarmNotificationEvent(alarm.Id));
                return;
            }

            var silenceEndTime = alarmRule.GetSilenceEndTime(alarm.LastNotificationTime.Value);

            if (DateTimeOffset.Now > silenceEndTime)
            {
                await _eventBus.PublishAsync(new SendAlarmNotificationEvent(alarm.Id));
            }
        }
    }
}
