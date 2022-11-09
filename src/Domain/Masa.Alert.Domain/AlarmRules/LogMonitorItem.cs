// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class LogMonitorItem : Entity<Guid>
{
    public string Field { get; protected set; } = default!;

    public LogAggregationTypes AggregationType { get; protected set; }

    public string Alias { get; protected set; } = default!;

    public bool IsOffset { get; protected set; }

    public int OffsetPeriod { get; protected set; }
}
