// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class SilenceCycle : ValueObject
{
    public SilenceCycleTypes Type { get; protected set; }

    public TimeInterval TimeInterval { get; protected set; } = default!;

    public int SilenceCycleValue { get; protected set; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Type;
        yield return TimeInterval;
        yield return SilenceCycleValue;
    }

    public SilenceCycle(int silenceCycleValue)
    {
        Type = SilenceCycleTypes.Cycle;

        SilenceCycleValue = silenceCycleValue;
    }

    public SilenceCycle(TimeInterval timeInterval)
    {
        Type = SilenceCycleTypes.Time;

        TimeInterval = timeInterval;
    }
}