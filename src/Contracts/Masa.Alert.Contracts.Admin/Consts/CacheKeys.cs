// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Contracts.Admin.Consts;

public class CacheKeys
{
    public const string CLIENT_CREDENTIALS_TOKEN = "client_credentials_token:";

    public static string ClientCredentialsTokenKey(string clientId)
    {
        return $"{CLIENT_CREDENTIALS_TOKEN}{clientId}";
    }
}