// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleItemViewModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public string Expression { get; set; } = string.Empty;

    public AlertSeverity AlertSeverity { get; set; }

    public bool IsRecoveryNotification { get; set; }

    public bool IsNotification { get; set; }

    public NotificationConfigViewModel RecoveryNotificationConfig { get; set; } = new();

    public NotificationConfigViewModel NotificationConfig { get; set; } = new();
}
