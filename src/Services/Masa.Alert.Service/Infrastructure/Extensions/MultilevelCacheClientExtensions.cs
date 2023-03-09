// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Infrastructure.Extensions;

public static class MultilevelCacheClientExtensions
{
    public static async Task<UserModel?> GetUserAsync(this IMultilevelCacheClient client, Guid userId)
    {
        return await client.GetAsync<UserModel>(CacheKeyConsts.UserKey(userId));
    }
}