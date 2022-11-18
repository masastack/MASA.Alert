// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class RuleResultItem : AlarmRuleItem
{
    public bool IsValid { get; set; }

    public RuleResultItem(string expression, AlertSeverity alertSeverity, bool isRecoveryNotification, bool isNotification, NotificationConfig recoveryNotificationConfig, NotificationConfig notificationConfig):base(expression, alertSeverity, isRecoveryNotification, isNotification, recoveryNotificationConfig, notificationConfig)
    {

    }
}
