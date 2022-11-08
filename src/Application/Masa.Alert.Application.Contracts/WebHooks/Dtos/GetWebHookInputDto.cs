// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.WebHooks.Dtos;

public class GetWebHookInputDto : PaginatedOptionsDto
{
    public string Filter { get; set; } = default!;

    public GetWebHookInputDto()
    {
    }

    public GetWebHookInputDto(int pageSize) : base("", 1, pageSize)
    {
    }
}
