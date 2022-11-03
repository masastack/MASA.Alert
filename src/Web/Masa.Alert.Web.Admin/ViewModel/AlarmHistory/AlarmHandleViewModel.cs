// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmHistory;

public class AlarmHandleViewModel
{
    public string MessageTemplateId { get; set; } = default!;

    public bool? IsThirdParty { get; set; }

    public Guid Handler { get; set; }

    public string WebHook { get; set; } = default!;

    public bool IsHandleNotice { get; set; }
}
