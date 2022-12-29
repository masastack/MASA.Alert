// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class AlarmHistoryDetail : AdminCompontentBase
{
    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    [Parameter]
    public AlarmHistoryViewModel AlarmHistory { get; set; } = new();

    [Parameter]
    public bool HandleStatusShow { get; set; }
}
