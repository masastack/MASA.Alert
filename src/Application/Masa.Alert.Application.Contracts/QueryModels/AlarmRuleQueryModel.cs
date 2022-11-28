﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleQueryModel : ISoftDelete
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public AlarmRuleTypes Type { get; set; }

    public string ProjectIdentity { get; set; } = string.Empty;

    public string AppIdentity { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public string ChartYAxisUnit { get; set; } = string.Empty;

    public int CheckFrequencyIntervalTime { get; set; }

    public TimeTypes CheckFrequencyIntervalTimeType { get; set; }

    public string CheckFrequencyCron { get; set; } = string.Empty;

    public AlarmCheckFrequencyTypes CheckFrequencyType { get; set; }

    public bool IsDeleted { get; set; }
}
