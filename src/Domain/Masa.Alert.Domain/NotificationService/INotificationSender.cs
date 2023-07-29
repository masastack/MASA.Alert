// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.NotificationService;

public interface INotificationSender
{
    Task SendAsync(NotificationConfig notificationConfig, Dictionary<string, object> variables);
}