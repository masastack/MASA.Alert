// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.WebHooks.Dtos;

public class WebHookDto : AuditEntityDto<Guid, Guid>
{
    public string DisplayName { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string SecretKey { get; set; } = default!;
}
