﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class CheckFrequency : ValueObject
{
    public AlarmCheckFrequencyTypes Type { get; protected set; }

    public TimeInterval FixedInterval { get; protected set; } = default!;

    public string CronExpression { get; protected set; } = default!;

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Type;
        yield return FixedInterval;
        yield return CronExpression;
    }

    public CheckFrequency(TimeInterval fixedInterval)
    {
        Type = AlarmCheckFrequencyTypes.FixedInterval;

        FixedInterval = fixedInterval;
    }

    public CheckFrequency(string cronExpression)
    {
        Type = AlarmCheckFrequencyTypes.Cron;

        CronExpression = cronExpression;
    }
}