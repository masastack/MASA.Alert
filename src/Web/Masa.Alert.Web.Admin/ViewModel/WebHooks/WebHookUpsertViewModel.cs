// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks;

public class WebHookUpsertViewModel
{
    public string DisplayName { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string SecretKey { get; set; } =  Guid.NewGuid().ToString();
}
