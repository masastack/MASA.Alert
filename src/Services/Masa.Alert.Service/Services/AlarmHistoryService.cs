// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class AlarmHistoryService : ServiceBase
{
    public AlarmHistoryService(IServiceCollection services) : base()
    {

    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<AlarmHistoryDto>> GetListAsync(IEventBus eventbus, [FromQuery] Guid? alarmRuleId, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime, [FromQuery] Guid? handler, [FromQuery] AlarmHistorySearchTypes searchType = default, [FromQuery] AlarmHistorySearchTimeTypes timeType = default, [FromQuery] AlertSeverity alertSeverity = default, [FromQuery] AlarmHistoryHandleStatuses status = default, [FromQuery] string filter = "", [FromQuery] string sorting = "", [FromQuery] int page = 1, [FromQuery] int pagesize = 10)
    {
        var inputDto = new GetAlarmHistoryInputDto(filter, alarmRuleId, searchType, timeType, startTime, endTime, alertSeverity, status, handler ?? default, sorting, page, pagesize);
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

    public async Task DeleteAsync(IEventBus eventBus, Guid id)
    {
        var command = new DeleteAlarmHistoryCommand(id);
        await eventBus.PublishAsync(command);
    }

    [RoutePattern("{id}/handle", StartWithBaseUri = true, HttpMethod = "Post")]
    public async Task HandleAsync(IEventBus eventBus, Guid id, [FromBody] AlarmHandleDto inputDto)
    {
        var command = new HandleAlarmHistoryCommand(id, inputDto);
        await eventBus.PublishAsync(command);
    }
}
