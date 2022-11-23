// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class MetricMonitorItemViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public MetricComparisonOperator ComparisonOperator { get; set; } = MetricComparisonOperator.GreaterThan;

    public string Value { get; set; } = string.Empty;

    public MetricAggregationTypes AggregationType { get; set; } = MetricAggregationTypes.BeEqualTo;

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }

    public List<string> TagItems { get; set; } = new();

    public List<string> ValueItems { get; set; } = new();

    public Dictionary<string, List<string>> LabelValues = new();
}
