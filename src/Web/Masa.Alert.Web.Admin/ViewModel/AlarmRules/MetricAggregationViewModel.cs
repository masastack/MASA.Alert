// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class MetricAggregationViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public int ComparisonOperator { get; set; } = MetricComparisonOperator.GreaterThan.Id;

    public string Value { get; set; } = string.Empty;

    public int AggregationType { get; set; } = MetricAggregationTypes.Count.Id;

    public List<string> TagItems { get; set; } = new();

    public List<string> ValueItems { get; set; } = new();

    public Dictionary<string, List<string>> LabelValues = new();
}
