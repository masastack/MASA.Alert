// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory;

public class AlarmHandleStatusCommitViewModel
{
    public AlarmHistoryHandleStatuses Status { get; set; }

    public DateTimeOffset CreationTime { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Remarks { get; set; } = string.Empty;
}
