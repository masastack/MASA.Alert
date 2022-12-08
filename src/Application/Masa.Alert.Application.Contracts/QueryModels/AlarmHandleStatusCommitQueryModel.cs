// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmHandleStatusCommitQueryModel
{
    public Guid Id { get; set; }

    public Guid AlarmHistoryId { get; set; }

    public AlarmHistoryHandleStatuses Status { get; set; }

    public DateTimeOffset CreationTime { get; set; }

    public Guid UserId { get; set; }

    public string Remarks { get; set; } = string.Empty;
}
