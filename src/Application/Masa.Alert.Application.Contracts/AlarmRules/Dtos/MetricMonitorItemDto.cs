// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class MetricMonitorItemDto
{
    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public MetricComparisonOperator ComparisonOperator { get; set; }

    public string Value { get; set; } = string.Empty;

    public MetricAggregationTypes AggregationType { get; set; }

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
