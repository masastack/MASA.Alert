// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.EventHandler;

public class AlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRulerepository;

    public AlarmEventHandler(IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRulerepository)
    {
        _repository = repository;
        _alarmRulerepository = alarmRulerepository;
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

        var alarmRule = await _alarmRulerepository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule != null)
        {
            if (alarm.LastNotificationTime == null)
            {
                // 通知
                return;
            }

            var silenceEndTime = alarmRule.GetSilenceEndTime(alarm.LastNotificationTime.Value);
            if (DateTimeOffset.Now > silenceEndTime)
            {
                // 通知
                return;
            }
        }
    }
}
