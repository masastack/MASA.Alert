// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class MetricAggregation : ValueObject
{
    public string Name { get; protected set; } = string.Empty;

    public string Tag { get; protected set; } = string.Empty;

    public MetricComparisonOperator ComparisonOperator { get; protected set; }

    public string Value { get; protected set; } = string.Empty;

    public MetricAggregationTypes AggregationType { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Name;
        yield return Tag;
        yield return ComparisonOperator;
        yield return Value;
        yield return AggregationType;
    }

    public MetricAggregation(string name, string tag, MetricComparisonOperator comparisonOperator, string value, MetricAggregationTypes aggregationType)
    {
        Name = name;
        Tag = tag;
        ComparisonOperator = comparisonOperator;
        Value = value;
        AggregationType = aggregationType;
    }
}
