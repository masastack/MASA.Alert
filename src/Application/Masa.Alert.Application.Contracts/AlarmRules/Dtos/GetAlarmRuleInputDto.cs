// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class GetAlarmRuleInputDto : PaginatedOptionsDto
{
    public string Filter { get; set; } = default!;

    public AlarmRuleTypes AlarmRuleType { get; set; } = AlarmRuleTypes.Log;

    public AlarmRuleSearchTimeTypes TimeType { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string ProjectId { get; set; } = default!;

    public string AppId { get; set; } = default!;

    public string MetricId { get; set; } = default!;

    public GetAlarmRuleInputDto()
    {
    }

    public GetAlarmRuleInputDto(int pageSize) : base("", 1, pageSize)
    {
    }

    public GetAlarmRuleInputDto(string filter, AlarmRuleSearchTimeTypes timeType, DateTime? startTime,
       DateTime? endTime, string projectId, string appId, string sorting, int page, int pageSize) : base(sorting, page, pageSize)
    {
        Filter = filter;
        TimeType = timeType;
        StartTime = startTime;
        EndTime = endTime;
        ProjectId = projectId;
        AppId = appId;
    }

    public enum AlarmRuleSearchTimeTypes
    {
        ModificationTime = 1,
        CreationTime,
    }
}