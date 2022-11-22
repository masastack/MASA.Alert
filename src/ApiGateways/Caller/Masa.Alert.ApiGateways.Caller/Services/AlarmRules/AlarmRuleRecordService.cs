// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.ApiGateways.Caller.Services.AlarmRules;

public class AlarmRuleRecordService : ServiceBase
{
    protected override string BaseUrl { get; set; }

    public AlarmRuleRecordService(ICaller caller) : base(caller)
    {
        BaseUrl = "api/v1/AlarmRuleRecords";
    }

    public async Task<PaginatedListDto<AlarmRuleRecordDto>> GetListAsync(GetAlarmRuleRecordInputDto inputDto)
    {
        return await GetAsync<GetAlarmRuleRecordInputDto, PaginatedListDto<AlarmRuleRecordDto>>(string.Empty, inputDto) ?? new();
    }
}