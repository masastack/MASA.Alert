// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks;

public class WebHookUpsertViewModel
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string SecretKey { get; set; } =  Guid.NewGuid().ToString();
}
