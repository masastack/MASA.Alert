// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class GetAlarmRuleRecordInputDto : PaginatedOptionsDto
{
    public Guid AlarmHistoryId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public GetAlarmRuleRecordInputDto()
    {
    }

    public GetAlarmRuleRecordInputDto(int pageSize) : base("", 1, pageSize)
    {
    }

    public GetAlarmRuleRecordInputDto(Guid alarmHistoryId, DateTime? startTime, DateTime? endTime, string sorting, int page, int pageSize) : base(sorting, page, pageSize)
    {
        AlarmHistoryId = alarmHistoryId;
        StartTime = startTime;
        EndTime = endTime;
    }
}
