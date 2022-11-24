﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class MetricMonitorItemViewModel
{
    public bool IsExpression { get; set; }

    public string Expression { get; set; } = string.Empty;

    public MetricAggregationViewModel Aggregation { get; set; } = new();

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
