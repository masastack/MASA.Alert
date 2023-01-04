// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmRecoveryNotificationEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmHistoryRepository _repository;

    public SendAlarmRecoveryNotificationEventHandler(INotificationSender notificationSender
        , IAlarmHistoryRepository repository)
    {
        _notificationSender = notificationSender;
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(SendAlarmRecoveryNotificationEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmHistoryId);
        if (alarm == null) return;

        foreach (var item in alarm.RuleResultItems)
        {
            if (!item.IsValid || !item.AlarmRuleItem.IsRecoveryNotification) continue;

            var notificationConfig = item.AlarmRuleItem.RecoveryNotificationConfig;

            await _notificationSender.SendAsync(notificationConfig);
        }
    }
}
