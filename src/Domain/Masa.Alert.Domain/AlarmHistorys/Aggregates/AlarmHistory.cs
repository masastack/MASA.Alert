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

    public AlarmHistoryHandleStatuses HandleStatus { get; protected set; }

    public DateTimeOffset? RecoveryTime { get; protected set; }

    public DateTimeOffset? LastNotificationTime { get; protected set; }

    public long Duration { get; protected set; }

    public bool IsNotification { get; protected set; }

    public List<RuleResultItem> RuleResultItems { get; protected set; } = new();

    public AlarmHandle Handle { get; protected set; } = default!;

    public IReadOnlyCollection<AlarmHandleStatusCommit> HandleStatusCommits => _handleStatusCommits.AsReadOnly();

    private List<AlarmHandleStatusCommit> _handleStatusCommits = new();

    private AlarmHistory() { }

    public AlarmHistory(Guid alarmRuleId, AlertSeverity alertSeverity, bool isNotification, List<RuleResultItem> ruleResultItems)
    {
        AlarmRuleId = alarmRuleId;
        AlertSeverity = alertSeverity;
        IsNotification = isNotification;
        RuleResultItems = ruleResultItems;
        HandleStatus = AlarmHistoryHandleStatuses.Pending;
        AlarmCount = 1;
        FirstAlarmTime = DateTimeOffset.Now;
        LastAlarmTime = DateTimeOffset.Now;
    }

    public void Recovery()
    {
        RecoveryTime = DateTimeOffset.Now;
        Duration = (long)(RecoveryTime - FirstAlarmTime).Value.TotalSeconds;
    }

    public void Update(AlertSeverity alertSeverity, bool isNotification, List<RuleResultItem> ruleResultItems)
    {
        AlertSeverity = alertSeverity;
        IsNotification = isNotification;
        RuleResultItems = ruleResultItems;
        AlarmCount++;
        LastAlarmTime = DateTimeOffset.Now;
    }

    public void Notification()
    {
        LastNotificationTime = DateTimeOffset.Now;
    }

    public void SetIsNotification(bool isNotification)
    {
        IsNotification = isNotification;
    }
}
