// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using System.Reflection.Metadata;

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

        if (Id == default)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            AddDomainEvent(new UpdateAlarmRuleRecordAlarmIdEvent(AlarmRuleId, Id));
        }
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

        AddDomainEvent(new UpdateAlarmRuleRecordAlarmIdEvent(AlarmRuleId, Id));
    }

    public void Update(AlertSeverity alertSeverity, bool isNotification, List<RuleResultItem> ruleResultItems)
    {
        AlertSeverity = alertSeverity;
        IsNotification = isNotification;
        RuleResultItems = ruleResultItems;
        AlarmCount++;
        LastAlarmTime = DateTimeOffset.Now;

        AddDomainEvent(new UpdateAlarmRuleRecordAlarmIdEvent(AlarmRuleId, Id));
    }

    public void Notification()
    {
        LastNotificationTime = DateTimeOffset.Now;
    }

    public void SetIsNotification(bool isNotification)
    {
        IsNotification = isNotification;
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
            AddDomainEvent(new NoticeAlarmHandleEvent(Handle));

            _handleStatusCommits.Add(new AlarmHandleStatusCommit(AlarmHistoryHandleStatuses.Notified, operatorId, Handle.NotificationConfig.TemplateName));
        }

        Recovery(false);
    }

    public void HandlerChange(Guid handler, string remark)
    {
        var commit = Handle.HandlerChange(handler, remark);
        _handleStatusCommits.Add(commit);
    }
}
