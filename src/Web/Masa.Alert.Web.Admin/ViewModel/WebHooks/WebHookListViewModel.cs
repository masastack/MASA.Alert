// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.WebHooks;

public class WebHookListViewModel
{
    public string DisplayName { get; set; } = default!;

    public string Url { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string SecretKey { get; set; } = default!;

    public DateTime ModificationTime { get; set; }

    public string ModifierName { get; set; } = string.Empty;
}
