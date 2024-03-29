﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class NotificationConfigDto
{
    public string ChannelCode { get; set; } = default!;

    public string TemplateCode { get; set; } = default!;

    public string TemplateName { get; set; } = default!;

    public int ChannelType { get; set; }

    public List<Guid> Receivers { get; set; } = new();
}
