// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class LogMonitorItemQueryModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public string Field { get; set; } = default!;

    public LogAggregationTypes AggregationType { get; set; }

    public string Alias { get; set; } = default!;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
