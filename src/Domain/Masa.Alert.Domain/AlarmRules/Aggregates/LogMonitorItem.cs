// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class LogMonitorItem : ValueObject
{
    public string Field { get; protected set; } = default!;

    public LogAggregationType AggregationType { get; protected set; }

    public string Alias { get; protected set; } = default!;

    public bool IsOffset { get; protected set; }

    public int OffsetPeriod { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Field;
        yield return AggregationType;
        yield return Alias;
        yield return IsOffset;
        yield return OffsetPeriod;
    }

    public LogMonitorItem(string field, LogAggregationType aggregationType, string alias, bool isOffset, int offsetPeriod)
    {
        Field = field;
        AggregationType = aggregationType;
        Alias = alias;
        IsOffset = isOffset;
        OffsetPeriod = offsetPeriod;
    }
}
