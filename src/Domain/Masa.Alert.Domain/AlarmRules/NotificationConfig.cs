// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class NotificationConfig
{
    public string MessageTemplateCode { get; set; } = default!;

    public List<Guid> Receivers { get; set; } = new();
}
