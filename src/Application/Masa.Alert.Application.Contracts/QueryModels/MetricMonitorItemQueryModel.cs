// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class MetricMonitorItemQueryModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public bool IsExpression { get; set; }

    public string Expression { get; set; } = string.Empty;

    public MetricAggregationQueryModel Aggregation { get; set; } = default!;

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
