﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmHistories.Dtos;

public class AlarmHistoryDto : AuditEntityDto<Guid, Guid>
{
    public Guid AlarmRuleId { get; set; }

    public AlarmRuleDto AlarmRule { get; set; } = new();

    public AlertSeverity AlertSeverity { get; set; }

    public DateTimeOffset FirstAlarmTime { get; set; }

    public int AlarmCount { get; set; }

    public DateTimeOffset LastAlarmTime { get; set; }

    public AlarmHistoryHandleStatuses HandleStatus { get; set; }

    public DateTimeOffset? RecoveryTime { get; set; }

    public DateTimeOffset? LastNotificationTime { get; set; }

    public List<RuleResultItemDto> RuleResultItems { get; set; } = new();

    public AlarmHandleDto Handle { get; set; } = new();

    public List<AlarmHandleStatusCommitDto> HandleStatusCommits { get; set; } = new();

    public bool IsNotification { get; set; }
}
