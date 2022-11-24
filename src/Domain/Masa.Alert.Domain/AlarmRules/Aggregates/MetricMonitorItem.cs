// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class MetricMonitorItem : ValueObject
{
    public bool IsExpression { get; protected set; }

    public string Expression { get; protected set; } = string.Empty;

    public MetricAggregation Aggregation { get; set; }

    public string Alias { get; protected set; } = string.Empty;

    public bool IsOffset { get; protected set; }

    public int OffsetPeriod { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Alias;
    }

    public MetricMonitorItem(bool isExpression, string expression, MetricAggregation aggregation, string alias, bool isOffset, int offsetPeriod)
    {
        IsExpression = isExpression;
        Expression = expression;
        Aggregation = aggregation;
        Alias = alias;
        IsOffset = isOffset;
        OffsetPeriod = offsetPeriod;
    }

    public string GetExpression()
    {
        if (IsExpression)
        {
            return Expression;
        }

        return Aggregation.GetExpression();
    }
}