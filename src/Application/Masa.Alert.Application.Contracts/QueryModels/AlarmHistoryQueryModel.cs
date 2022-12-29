// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmHistoryQueryModel : ISoftDelete
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public AlarmRuleQueryModel AlarmRule { get; set; } = new();

    public AlertSeverity AlertSeverity { get; set; }

    public DateTimeOffset FirstAlarmTime { get; set; }

    public int AlarmCount { get; set; }

    public DateTimeOffset LastAlarmTime { get; set; }

    public DateTimeOffset? RecoveryTime { get; set; }

    public DateTimeOffset? LastNotificationTime { get; set; }

    public bool IsNotification { get; set; }

    public DateTime ModificationTime { get; set; }

    public Guid Handler { get; set; }

    public Guid WebHookId { get; set; }

    public AlarmHistoryHandleStatuses HandleStatus { get; set; }

    public bool IsHandleNotice { get; set; }

    public NotificationConfigQueryModel HandleNotificationConfig { get; set; } = new();

    public List<RuleResultItemQueryModel> RuleResultItems { get; set; } = new();

    public List<AlarmHandleStatusCommitQueryModel> HandleStatusCommits { get; set; } = new();

    public bool IsDeleted { get; set; }

    public Guid Modifier { get; set; }
}
