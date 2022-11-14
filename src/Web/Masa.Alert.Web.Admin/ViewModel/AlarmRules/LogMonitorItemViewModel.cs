// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class LogMonitorItemViewModel
{
    public string Field { get; set; } = string.Empty;

    public LogAggregationTypes AggregationType { get; set; } = LogAggregationTypes.Count;

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
