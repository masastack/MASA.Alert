// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.Repositories;

public static class AlarmRuleEfCoreQueryableExtensions
{
    public static IQueryable<AlarmRule> IncludeDetails(this IQueryable<AlarmRule> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }
        return queryable.Include(x => x.Items);
    }
}
