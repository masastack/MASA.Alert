// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public record GetAlarmRuleRecordListQuery(GetAlarmRuleRecordInputDto Input) : Query<PaginatedListDto<AlarmRuleRecordDto>>
{
    public override PaginatedListDto<AlarmRuleRecordDto> Result { get; set; } = new();
}
