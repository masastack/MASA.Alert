// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.Queries;

public record GetWebHookQuery(Guid WebHookId) : Query<WebHookDto>
{
    public override WebHookDto Result { get; set; } = new();
}
