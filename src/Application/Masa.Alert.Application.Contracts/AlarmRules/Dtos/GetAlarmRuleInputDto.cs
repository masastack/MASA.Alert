// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Domain.Shared.AlarmRules;

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class GetAlarmRuleInputDto : PaginatedOptionsDto
{
    public string Filter { get; set; } = default!;

    public AlarmRuleTypes AlarmRuleType { get; set; } = AlarmRuleTypes.Log;

    public AlarmRuleSearchTimeTypes TimeType { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string ProjectIdentity { get; set; } = default!;

    public string AppIdentity { get; set; } = default!;

    public string MetricId { get; set; } = default!;

    public GetAlarmRuleInputDto()
    {
    }

    public GetAlarmRuleInputDto(int pageSize) : base("", 1, pageSize)
    {
    }

    public GetAlarmRuleInputDto(string filter, AlarmRuleTypes alarmRuleType, AlarmRuleSearchTimeTypes timeType, DateTime? startTime,
       DateTime? endTime, string projectIdentity, string appIdentity, string metricId, string sorting, int page, int pageSize) : base(sorting, page, pageSize)
    {
        Filter = filter;
        AlarmRuleType = alarmRuleType;
        TimeType = timeType;
        StartTime = startTime;
        EndTime = endTime;
        ProjectIdentity = projectIdentity;
        AppIdentity = appIdentity;
        MetricId = metricId;
    }

    public enum AlarmRuleSearchTimeTypes
    {
        ModificationTime = 1,
        CreationTime,
    }
}