// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.Aggregates;

public class AlarmHistory : FullAggregateRoot<Guid, Guid>
{
    public Guid AlarmRuleId { get; protected set; }

    public AlertSeverity AlertSeverity { get; protected set; }

    public DateTimeOffset FirstAlarmTime { get; protected set; }

    public int AlarmCount { get; protected set; }

    public DateTimeOffset LastAlarmTime { get; protected set; }

    public AlarmHistoryStatuses Status { get; protected set; }

    public DateTimeOffset? RecoveryTime { get; protected set; }

    public List<AlarmRuleItem> AlarmRuleItems { get; protected set; } = new();

    private AlarmHistory() { }

    public AlarmHistory(Guid alarmRuleId, AlertSeverity alertSeverity, List<AlarmRuleItem> alarmRuleItems)
    {
        AlarmRuleId = alarmRuleId;
        AlertSeverity = alertSeverity;
        AlarmRuleItems = alarmRuleItems;
        Status = AlarmHistoryStatuses.Pending;
        AlarmCount = 1;
        FirstAlarmTime = DateTimeOffset.Now;
        LastAlarmTime = DateTimeOffset.Now;
    }

    public void Recovery()
    {
        RecoveryTime = DateTimeOffset.Now;
    }
}
