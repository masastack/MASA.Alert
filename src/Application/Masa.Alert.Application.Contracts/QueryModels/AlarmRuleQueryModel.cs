// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleQueryModel : ISoftDelete
{
    public Guid Id { get; set; }

    public AlarmRuleTypes Type { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string ProjectIdentity { get; set; } = string.Empty;

    public string AppIdentity { get; set; } = string.Empty;

    public string ChartYAxisUnit { get; set; } = string.Empty;

    public CheckFrequencyQueryModel CheckFrequency { get; set; } = new();

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string WhereExpression { get; set; } = string.Empty;

    public int ContinuousTriggerThreshold { get; set; }

    public SilenceCycleQueryModel SilenceCycle { get; set; } = new();

    public bool IsEnabled { get; set; }

    public List<LogMonitorItemQueryModel> LogMonitorItems { get; set; } = new();

    public List<MetricMonitorItemQueryModel> MetricMonitorItems { get; set; } = new();

    public List<AlarmRuleItemQueryModel> Items { get; set; } = new();

    public bool IsDeleted { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime ModificationTime { get; set; }

    public Guid Modifier { get; set; }

    public string Source { get; set; } = default!;

    public bool Show { get; set; }
}
