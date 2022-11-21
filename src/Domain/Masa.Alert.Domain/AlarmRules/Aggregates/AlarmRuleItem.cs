// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class AlarmRuleItem : ValueObject
{
    public Guid AlarmRuleId { get; protected set; }

    public string Expression { get; protected set; } = default!;

    public AlertSeverity AlertSeverity { get; protected set; }

    public bool IsRecoveryNotification { get; protected set; }

    public bool IsNotification { get; protected set; }

    public NotificationConfig RecoveryNotificationConfig { get; protected set; } = new();

    public NotificationConfig NotificationConfig { get; protected set; } = new();

    public AlarmRuleItem(Guid alarmRuleId, string expression, AlertSeverity alertSeverity, bool isRecoveryNotification, bool isNotification, NotificationConfig recoveryNotificationConfig, NotificationConfig notificationConfig)
    {
        AlarmRuleId = alarmRuleId;
        Expression = expression;
        AlertSeverity = alertSeverity;
        IsRecoveryNotification = isRecoveryNotification;
        IsNotification = isNotification;
        NotificationConfig = notificationConfig;
        RecoveryNotificationConfig = recoveryNotificationConfig;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return AlarmRuleId;
        yield return AlertSeverity;
        yield return IsRecoveryNotification;
        yield return IsNotification;
        yield return RecoveryNotificationConfig;
        yield return NotificationConfig;
    }
}
