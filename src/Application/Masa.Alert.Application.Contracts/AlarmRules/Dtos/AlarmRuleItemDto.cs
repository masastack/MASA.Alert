// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class AlarmRuleItemDto : EntityDto<Guid>
{
    public Guid AlarmRuleId { get; set; }

    public string Expression { get; set; } = string.Empty;

    public AlertSeverity AlertSeverity { get; set; }

    public bool IsRecoveryNotification { get; set; }

    public bool IsNotification { get; set; }

    public NotificationConfigDto RecoveryNotificationConfig { get; set; } = new();

    public NotificationConfigDto NotificationConfig { get; set; } = new();
}
