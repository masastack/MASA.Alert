// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class AlarmRuleUpsertDto
{
    public string DisplayName { get; set; } = string.Empty;

    public AlarmRuleTypes Type { get; set; }

    public string ProjectIdentity { get; set; } = string.Empty;

    public string AppIdentity { get; set; } = string.Empty;

    public int Step { get; set; } = 1;

    public string ChartYAxisUnit { get; set; } = string.Empty;

    public CheckFrequencyDto CheckFrequency { get; set; } = new();

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string WhereExpression { get; set; } = string.Empty;

    public string QueryStr { get; set; } = "Query";

    public int ContinuousTriggerThreshold { get; set; }

    public SilenceCycleDto SilenceCycle { get; set; } = new();

    public bool IsEnabled { get; set; }

    public List<LogMonitorItemDto> LogMonitorItems { get; set; } = new();

    public List<MetricMonitorItemDto> MetricMonitorItems { get; set; } = new();

    public List<AlarmRuleItemDto> Items { get; set; } = new();

    public string Source { get; set; } = default!;

    public bool Show { get; set; } = false;
}
