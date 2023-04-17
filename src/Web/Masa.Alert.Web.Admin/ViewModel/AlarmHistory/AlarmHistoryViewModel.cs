﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory;

public record class AlarmHistoryViewModel
{
    public Guid Id { get; set; }

    public AlertSeverity AlertSeverity { get; set; }

    public string DisplayName { get; set; } = default!;

    public string TriggerRules { get; set; } = default!;

    public DateTimeOffset FirstAlarmTime { get; set; }

    public int AlarmCount { get; set; }

    public DateTimeOffset LastAlarmTime { get; set; }

    public AlarmRuleViewModel AlarmRule { get; set; } = new();

    public List<RuleResultItemViewModel> RuleResultItems { get; set; } = new();

    public List<AlarmHandleStatusCommitViewModel> HandleStatusCommits { get; set; } = new();

    public AlarmHandleViewModel Handle { get; set; } = new();

    public bool IsNotification { get; set; }
}