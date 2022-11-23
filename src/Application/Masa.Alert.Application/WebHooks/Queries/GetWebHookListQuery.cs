// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.Queries;

public record GetWebHookListQuery(GetWebHookInputDto Input) : Query<PaginatedListDto<WebHookDto>>
{
    public override PaginatedListDto<WebHookDto> Result { get; set; } = new();
}
