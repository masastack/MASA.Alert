// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class TimeInterval : ValueObject
{
    public int IntervalTime { get; protected set; }

    public TimeTypes IntervalTimeType { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return IntervalTime;
        yield return IntervalTimeType;
    }

    public TimeInterval(int intervalTime, TimeTypes intervalTimeType)
    {
        IntervalTime = intervalTime;
        IntervalTimeType = intervalTimeType;
    }
}
