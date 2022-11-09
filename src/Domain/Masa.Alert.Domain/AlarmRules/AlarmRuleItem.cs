// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class AlarmRuleItem : Entity<Guid>
{
    public Guid AlarmRuleId { get; protected set; }

    public string Expression { get; protected set; } = default!;

    public AlertSeverity AlertSeverity { get; protected set; }

    public bool IsRecoveryNotification { get; protected set; }

    public bool IsNotification { get; protected set; }

    public NotificationConfig RecoveryNotificationConfig { get; protected set; } = new();

    public NotificationConfig NotificationConfig { get; protected set; } = new();
}
