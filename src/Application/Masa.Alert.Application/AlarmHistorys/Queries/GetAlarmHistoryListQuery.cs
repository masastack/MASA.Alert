// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistorys.Queries;

public record GetAlarmHistoryListQuery(GetAlarmHistoryInputDto Input) : Query<PaginatedListDto<AlarmHistoryDto>>
{
    public override PaginatedListDto<AlarmHistoryDto> Result { get; set; } = new();
}