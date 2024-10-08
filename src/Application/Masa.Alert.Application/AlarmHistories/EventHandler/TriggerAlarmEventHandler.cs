﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.EventHandler;

public class TriggerAlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRulerepository;

    public TriggerAlarmEventHandler(IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRulerepository)
    {
        _repository = repository;
        _alarmRulerepository = alarmRulerepository;
    }

    [EventHandler]
    public async Task HandleEventAsync(TriggerAlarmEvent eto)
    {
        var alarmRule = await _alarmRulerepository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule == null) return;

        var alarm = await _repository.GetLastAsync(eto.AlarmRuleId);
        var isNotification = alarmRule.CheckIsNotification();
        var isSilence = alarmRule.CheckIsSilence(alarm?.LastNotificationTime);

        if (alarm == null || alarm.RecoveryTime.HasValue)
        {
            alarm = new AlarmHistory(eto.AlarmRuleId, eto.AlertSeverity, isNotification, eto.TriggerRuleItems);
            alarm.AddAlarmRuleRecord(eto.ExcuteTime, eto.AggregateResult, true, eto.ConsecutiveCount, eto.TriggerRuleItems);
            alarm.SetIsNotification(isNotification, isSilence, eto.AggregateResult);
            await _repository.AddAsync(alarm);
        }
        else
        {
            alarm.Update(eto.AlertSeverity, isNotification, eto.TriggerRuleItems);
            alarm.SetIsNotification(isNotification, isSilence, eto.AggregateResult);
            alarm.AddAlarmRuleRecord(eto.ExcuteTime, eto.AggregateResult, true, eto.ConsecutiveCount, eto.TriggerRuleItems);
            await _repository.UpdateAsync(alarm);
            
        }
    }
}
