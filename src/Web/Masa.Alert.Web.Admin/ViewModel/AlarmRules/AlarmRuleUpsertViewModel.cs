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

    public AlarmRuleCheckFrequency CheckFrequency { get; set; }

    public int CheckIntervalTime { get; set; } = 15;

    public CheckFrequencyTimeTypes CheckIntervalTimeType { get; set; } = CheckFrequencyTimeTypes.Minute;

    public string CronExpression { get; set; } = default!;

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string Expression { get; set; } = default!;

    public List<LogMonitorItemViewModel> LogMonitorItems { get; set; }

    public List<MetricMonitorItemViewModel> MetricMonitorItems { get; set; } = default!;

    public AlarmRuleUpsertViewModel()
    {
        LogMonitorItems = LogMonitorItems ?? new List<LogMonitorItemViewModel> { new LogMonitorItemViewModel() };
    }
}
