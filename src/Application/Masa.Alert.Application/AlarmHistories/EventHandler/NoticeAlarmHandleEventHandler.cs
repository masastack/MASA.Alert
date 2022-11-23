// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.EventHandler;

public class NoticeAlarmHandleEventHandler
{
    private readonly INotificationSender _notificationSender;

    public NoticeAlarmHandleEventHandler(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    [EventHandler]
    public async Task HandleEventAsync(NoticeAlarmHandleEvent eto)
    {
        var notificationConfig = eto.AlarmHandle.NotificationConfig;

        await _notificationSender.SendAsync(notificationConfig);
    }
}
