// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleUpsertViewModel
{
    public AlarmRuleTypes AlarmRuleType { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string ProjectIdentity { get; set; } = string.Empty;

    public string AppIdentity { get; set; } = string.Empty;

    public int Step { get; set; } = 1;

    public string ChartYAxisUnit { get; set; } = string.Empty;

    public CheckFrequencyViewModel CheckFrequency { get;  set; } = new();

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string WhereExpression { get; set; } = string.Empty;

    public string QueryStr { get; set; } = "Query";

    public int ContinuousTriggerThreshold { get; set; }

    public SilenceCycleViewModel SilenceCycle { get;  set; } = new();

    public bool IsEnabled { get; set; }

    public List<LogMonitorItemViewModel> LogMonitorItems { get; set; } = new();

    public List<MetricMonitorItemViewModel> MetricMonitorItems { get; set; } = new();

    public List<AlarmRuleItemViewModel> Items { get; set; } = new();
}
