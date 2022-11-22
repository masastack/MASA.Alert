// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmHistorys.Dtos;

public class AlarmHandleDto
{
    public bool IsThirdParty { get; set; }

    public Guid Handler { get; set; }

    public Guid WebHookId { get; set; }

    public bool IsHandleNotice { get; set; }

    public NotificationConfigDto NotificationConfig { get; set; } = new();
}
