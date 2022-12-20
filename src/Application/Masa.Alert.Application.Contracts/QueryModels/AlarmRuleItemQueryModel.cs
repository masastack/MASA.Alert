// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleItemQueryModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public string Expression { get; set; } = default!;

    public AlertSeverity AlertSeverity { get; set; }

    public bool IsRecoveryNotification { get; set; }

    public bool IsNotification { get; set; }

    public NotificationConfigQueryModel RecoveryNotificationConfig { get; set; } = new();

    public NotificationConfigQueryModel NotificationConfig { get; set; } = new();
}