// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleListViewModel
{
    public Guid Id { get; set; }

    public AlarmRuleTypes AlarmRuleType { get; set; }

    public string DisplayName { get; set; } = default!;

    public string ProjectIdentity { get; set; } = default!;

    public string AppIdentity { get; set; } = default!;

    public DateTime ModificationTime { get; set; }

    public string ModifierName { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public List<MetricMonitorItemViewModel> MetricMonitorItems { get; set; } = new();
}
