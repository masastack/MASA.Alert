// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleUpsertViewModel
{
    public string DisplayName { get; set; } = default!;

    public string ProjectId { get; set; } = default!;

    public string AppId { get; set; } = default!;

    public int Step { get; set; } = 1;

    public string ChartYAxisUnit { get; set; } = default!;

    public AlarmCheckFrequencyTypes CheckFrequency { get; set; }

    public int CheckIntervalTime { get; set; } = 15;

    public TimeTypes CheckIntervalTimeType { get; set; } = TimeTypes.Minute;

    public string CronExpression { get; set; } = default!;

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string WhereExpression { get; set; } = default!;

    public string QueryStr { get; set; } = "Query";

    public int ContinuousTriggerThreshold { get; set; }

    public AlarmRuleSilenceCycle SilenceCycle { get; set; }

    public int SilenceTimeValue { get; set; } = 15;

    public TimeTypes SilenceTimeType { get; set; } = TimeTypes.Minute;

    public int SilenceCycleValue { get; set; } = 15;

    public bool IsEnabled { get; set; }

    public List<LogMonitorItemViewModel> LogMonitorItems { get; set; } = new();

    public List<MetricMonitorItemViewModel> MetricMonitorItems { get; set; } = new();

    public List<AlarmRuleItemViewModel> Items { get; set; } = new();
}
