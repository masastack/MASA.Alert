// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class TimeInterval : ValueObject
{
    public int IntervalTime { get; protected set; }

    public TimeType IntervalTimeType { get;}

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return IntervalTime;
        yield return IntervalTimeType;
    }
    
    private TimeInterval() { }

    public TimeInterval(int intervalTime, string intervalTimeType)
    {
        IntervalTime = intervalTime;
        IntervalTimeType = TimeType.StartNew(intervalTimeType);
    }

    public TimeSpan GetIntervalTime()
    {
        return IntervalTimeType.GetIntervalTime(IntervalTime);
    }

    public string GetCronExpression()
    {
        return IntervalTimeType.GetCronExpression(IntervalTime);
    }
}
