// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Infrastructure.Ddd.Application.Contracts.Dtos;

public class PaginatedOptionsDto<T> : FromUri<T>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string Sorting { get; set; }

    public PaginatedOptionsDto(string sorting = "", int page = 1, int pageSize = 10)
    {
        Sorting = sorting;
        Page = page;
        PageSize = pageSize;
    }

    public Dictionary<string, bool>? ApplySorting()
    {
        if (string.IsNullOrWhiteSpace(Sorting))
        {
            return new Dictionary<string, bool>();
        }

        return Sorting.Split(',')
            .Select(i => new KeyValuePair<string, bool>(i.Split(' ')[0], i.Split(' ')[1].ToLower() == "desc"))
            .ToDictionary(k => k.Key, k => k.Value); 
    }
}