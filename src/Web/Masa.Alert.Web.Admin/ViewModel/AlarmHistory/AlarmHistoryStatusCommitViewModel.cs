// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory;

public class AlarmHistoryStatusCommitViewModel
{
    public AlarmHistoryStatuses Status { get; set; }

    public string Operator { get; set; } = default!;

    public string Remarks { get; set; } = default!;

    public DateTime CreationTime { get; set; }
}
