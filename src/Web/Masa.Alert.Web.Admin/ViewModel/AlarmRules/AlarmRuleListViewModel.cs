// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleListViewModel
{
    public string DisplayName { get; set; } = default!;

    public string ProjectId { get; set; } = default!;

    public string AppId { get; set; } = default!;

    public DateTime ModificationTime { get; set; }

    public string ModifierName { get; set; } = string.Empty;
}
