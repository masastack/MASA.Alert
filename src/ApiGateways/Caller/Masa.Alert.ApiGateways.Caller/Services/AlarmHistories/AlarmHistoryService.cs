// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.ApiGateways.Caller.Services.AlarmHistories;

public class AlarmHistoryService : ServiceBase
{
    protected override string BaseUrl { get; set; }

    public AlarmHistoryService(ICaller caller) : base(caller)
    {
        BaseUrl = "api/v1/AlarmHistories";
    }

    public async Task<AlarmHistoryDto> GetAsync(Guid id)
    {
        return await GetAsync<AlarmHistoryDto>($"{id}");
    }

    public async Task<PaginatedListDto<AlarmHistoryDto>> GetListAsync(GetAlarmHistoryInputDto inputDto)
    {
        return await GetAsync<GetAlarmHistoryInputDto, PaginatedListDto<AlarmHistoryDto>>(string.Empty, inputDto) ?? new();
    }

    public async Task HandleAsync(Guid id, AlarmHandleDto inputDto)
    {
        await PostAsync($"{id}/handle", inputDto);
    }
}