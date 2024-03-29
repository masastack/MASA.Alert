﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class LogMonitorItemDto
{
    public string Field { get; set; } = string.Empty;

    public LogAggregationTypes AggregationType { get; set; }

    public string Alias { get; set; } = string.Empty;

    public bool IsOffset { get; set; }

    public int OffsetPeriod { get; set; }
}
