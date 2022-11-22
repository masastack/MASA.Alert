// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class AlarmHistoryService : ServiceBase
{
    public AlarmHistoryService(IServiceCollection services) : base()
    {

    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<AlarmHistoryDto>> GetListAsync(IEventBus eventbus, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime, [FromQuery] AlarmHistorySearchTypes searchType = default, [FromQuery] AlarmHistorySearchTimeTypes timeType = default, [FromQuery] AlertSeverity alertSeverity = default, [FromQuery] AlarmHistoryHandleStatuses status = default, [FromQuery] string filter = "", [FromQuery] string sorting = "", [FromQuery] int page = 1, [FromQuery] int pagesize = 10)
    {
        var inputDto = new GetAlarmHistoryInputDto(filter, searchType, timeType, startTime, endTime, alertSeverity, status, sorting, page, pagesize);
        var query = new GetAlarmHistoryListQuery(inputDto);
        await eventbus.PublishAsync(query);
        return query.Result;
    }

    public async Task<AlarmHistoryDto> GetAsync(IEventBus eventBus, Guid id)
    {
        var query = new GetAlarmHistoryQuery(id);
        await eventBus.PublishAsync(query);
        return query.Result;
    }
}
