// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Aggregates;

public class AlarmHandleStatusCommit : ValueObject
{
    public AlarmHistoryHandleStatuses Status { get; protected set; }

    public DateTimeOffset CreationTime { get; protected set; }

    public Guid UserId { get; protected set; }

    public string Remarks { get; protected set; } = string.Empty;

    public AlarmHandleStatusCommit(AlarmHistoryHandleStatuses status, Guid userId, string remarks)
    {
        Status = status;
        CreationTime = DateTimeOffset.Now;
        UserId = userId;
        Remarks = remarks;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Status;
        yield return CreationTime;
        yield return UserId;
        yield return Remarks;
    }
}
