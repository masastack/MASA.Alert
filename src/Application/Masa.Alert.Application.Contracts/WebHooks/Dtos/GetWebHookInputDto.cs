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

    public GetWebHookInputDto(string filter, string sorting, int page, int pageSize) : base(sorting, page, pageSize)
    {
        Filter = filter;
    }
}
