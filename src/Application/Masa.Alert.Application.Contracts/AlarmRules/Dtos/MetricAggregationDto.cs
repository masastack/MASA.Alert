// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class MetricAggregationDto
{
    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public int ComparisonOperator { get; set; } = MetricComparisonOperator.GreaterThan.Id;

    public string Value { get; set; } = string.Empty;

    public int AggregationType { get; set; } = MetricAggregationTypes.Count.Id;
}
