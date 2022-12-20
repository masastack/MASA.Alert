// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class MetricAggregationQueryModel
{
    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public MetricComparisonOperators ComparisonOperator { get; set; }

    public string Value { get; set; } = string.Empty;

    public MetricAggregationTypes AggregationType { get; set; }
}
