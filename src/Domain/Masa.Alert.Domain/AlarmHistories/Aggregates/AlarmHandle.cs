// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Aggregates;

public class AlarmHandle : ValueObject
{
    public Guid Handler { get; protected set; }

    public Guid WebHookId { get; protected set; }

    public AlarmHistoryHandleStatuses Status { get; protected set; }

    public bool IsHandleNotice { get; protected set; }

    public NotificationConfig NotificationConfig { get; protected set; } = new();

    public AlarmHandle(Guid handler, Guid webHookId, bool isHandleNotice, NotificationConfig notificationConfig)
    {
        Handler = handler;
        WebHookId = webHookId;
        IsHandleNotice = isHandleNotice;
        NotificationConfig = notificationConfig;
        Status = AlarmHistoryHandleStatuses.Pending;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Handler;
        yield return WebHookId;
        yield return Status;
        yield return IsHandleNotice;
        yield return NotificationConfig;
    }

    public AlarmHandleStatusCommit HandleAlarm(Guid operatorId, string remark)
    {
        Status = AlarmHistoryHandleStatuses.InProcess;
        return new AlarmHandleStatusCommit(Status, operatorId, remark);
    }

    public AlarmHandleStatusCommit Completed(Guid operatorId, string remark)
    {
        Status = AlarmHistoryHandleStatuses.ProcessingCompleted;
        return new AlarmHandleStatusCommit(Status, operatorId, remark);
    }
}