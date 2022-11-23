// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmNotificationEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmHistoryRepository _repository;

    public SendAlarmNotificationEventHandler(INotificationSender notificationSender
        , IAlarmHistoryRepository repository)
    {
        _notificationSender = notificationSender;
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(SendAlarmNotificationEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmHistoryId);
        if (alarm == null) return;

        foreach (var item in alarm.RuleResultItems)
        {
            if (!item.AlarmRuleItem.IsNotification) continue;

            var notificationConfig = item.AlarmRuleItem.NotificationConfig;

            await _notificationSender.SendAsync(notificationConfig);
        }

        alarm.Notification();
        await _repository.UpdateAsync(alarm);
    }
}
