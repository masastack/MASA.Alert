// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class NotificationConfigViewModel
{
    public string MessageTemplateCode { get; set; } = default!;

    public string MessageTemplateName { get; set; } = default!;

    public List<Guid> Receivers { get; set; } = new();
}
