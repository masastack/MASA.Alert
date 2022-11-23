// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.WebHooks.Aggregates;

public class WebHook : FullAggregateRoot<Guid, Guid>
{
    public string DisplayName { get; protected set; } = default!;

    public string Url { get; protected set; } = default!;

    public string Description { get; protected set; } = default!;

    public string SecretKey { get; protected set; } = default!;

    public WebHook(string displayName, string url, string description, string secretKey)
    {
        DisplayName = displayName;
        Url = url;
        Description = description;
        SecretKey = secretKey;
    }

    public void GenerateSecretKey()
    {
        SecretKey = Guid.NewGuid().ToString();
    }
}