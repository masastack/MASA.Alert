// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Domain.Shared.Enums;

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class AlarmRuleDto : AuditEntityDto<Guid, Guid>
{
    public string DisplayName { get; set; } = default!;

    public string ProjectIdentity { get; set; } = default!;

    public string AppIdentity { get; set; } = default!;

    public bool IsEnabled { get; set; }

    public string ChartYAxisUnit { get; set; } = default!;

    public AlarmCheckFrequencyTypes CheckFrequency { get; set; }

    public int CheckIntervalTime { get; set; }

    public TimeTypes CheckIntervalTimeType { get; set; } = TimeTypes.Minute;

    public string CronExpression { get; set; } = default!;

    public bool IsGetTotal { get; set; }

    public string TotalVariable { get; set; } = "total";

    public string WhereExpression { get; set; } = default!;

    public int ContinuousTriggerThreshold { get; set; }

    public AlarmRuleSilenceCycle SilenceCycle { get; set; }

    public int SilenceTimeValue { get; set; }

    public TimeTypes SilenceTimeType { get; set; } = TimeTypes.Minute;

    public int SilenceCycleValue { get; set; }

    public List<LogMonitorItemDto> LogMonitorItems { get; protected set; } = new();

    public List<AlarmRuleItemDto> Items { get; protected set; } = new();
}
