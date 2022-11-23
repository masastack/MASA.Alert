// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class MetricMonitorItem : ValueObject
{
    public string Name { get; protected set; } = string.Empty;

    public string Tag { get; protected set; } = string.Empty;

    public MetricComparisonOperator ComparisonOperator { get; protected set; }

    public string Value { get; protected set; } = string.Empty;

    public MetricAggregationTypes AggregationType { get; protected set; }

    public string Alias { get; protected set; } = string.Empty;

    public bool IsOffset { get; protected set; }

    public int OffsetPeriod { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Name;
        yield return Tag;
        yield return ComparisonOperator;
        yield return Value;
        yield return AggregationType;
        yield return Alias;
        yield return IsOffset;
        yield return OffsetPeriod;
    }
    public MetricMonitorItem(string name, string tag, MetricComparisonOperator comparisonOperator, string value, MetricAggregationTypes aggregationType, string alias, bool isOffset, int offsetPeriod)
    {
        Name = name;
        Tag = tag;
        ComparisonOperator = comparisonOperator;
        Value = value;
        AggregationType = aggregationType;
        Alias = alias;
        IsOffset = isOffset;
        OffsetPeriod = offsetPeriod;
    }
}