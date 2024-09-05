// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Aggregates;

public class AlarmHistory : FullAggregateRoot<Guid, Guid>
{
    public Guid AlarmRuleId { get; protected set; }

    public AlertSeverity AlertSeverity { get; protected set; }

    public DateTimeOffset FirstAlarmTime { get; protected set; }

    public int AlarmCount { get; protected set; }

    public DateTimeOffset LastAlarmTime { get; protected set; }

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
        AlarmCount = 1;
        FirstAlarmTime = DateTimeOffset.Now;
        LastAlarmTime = DateTimeOffset.Now;

        Handle = new();
        _handleStatusCommits.Add(new AlarmHandleStatusCommit(AlarmHistoryHandleStatuses.Pending, default, string.Empty));
    }

    public void Recovery(bool isAuto)
    {
        RecoveryTime = DateTimeOffset.Now;
        Duration = (long)(RecoveryTime - FirstAlarmTime).Value.TotalSeconds;

        if (isAuto)
        {
            var commit = Handle.Completed(default, "自动恢复");
            _handleStatusCommits.Add(commit);

            AddDomainEvent(new SendAlarmRecoveryNotificationEvent(Id));
        }
    }

    public void Update(AlertSeverity alertSeverity, bool isNotification, List<RuleResultItem> ruleResultItems)
    {
        AlertSeverity = alertSeverity;
        IsNotification = isNotification;
        RuleResultItems = ruleResultItems;
        AlarmCount++;
        LastAlarmTime = DateTimeOffset.Now;
    }

    public void AddAlarmRuleRecord(DateTimeOffset excuteTime, ConcurrentDictionary<string, long> aggregateResult, bool isTrigger, int consecutiveCount, List<RuleResultItem> ruleResultItems)
    {
        if (Id == default)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
        }

        AddDomainEvent(new AddAlarmRuleRecordEvent(AlarmRuleId, Id, excuteTime, aggregateResult, isTrigger, consecutiveCount, ruleResultItems));
    }

    public void Notification()
    {
        LastNotificationTime = DateTimeOffset.Now;
    }

    public void SetIsNotification(bool isNotification, bool isSilence, ConcurrentDictionary<string, long> aggregateResult)
    {
        IsNotification = isNotification;

        if (IsNotification && !isSilence)
        {
            AddDomainEvent(new SendAlarmNotificationEvent(Id, AlarmRuleId, aggregateResult));
        }
    }

    public void HandleAlarm(AlarmHandle handle, Guid operatorId, string remark)
    {
        Handle = handle;

        if (Handle.WebHookId != default)
        {
            AddDomainEvent(new PostWebHookEvent(Id, Handle.WebHookId, Handle.Handler));

            var commit = Handle.HandleAlarm(operatorId, remark);
            _handleStatusCommits.Add(commit);
        }
        else
        {
            Completed(operatorId, remark);
        }
    }

    public void Completed(Guid operatorId, string remark)
    {
        var commit = Handle.Completed(operatorId, remark);
        _handleStatusCommits.Add(commit);

        if (Handle.IsHandleNotice)
        {
            AddDomainEvent(new NoticeAlarmHandleEvent(Handle, AlarmRuleId));

            _handleStatusCommits.Add(new AlarmHandleStatusCommit(AlarmHistoryHandleStatuses.Notified, operatorId, Handle.NotificationConfig.TemplateName));
        }

        Recovery(false);
    }

    public void ChangeHandler(Guid handler, string remark)
    {
        var commit = Handle.ChangeHandler(handler, remark);
        _handleStatusCommits.Add(commit);
    }
}
