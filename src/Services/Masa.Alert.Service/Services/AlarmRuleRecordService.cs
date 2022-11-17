// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class AlarmRuleRecordService : ServiceBase
{
    public AlarmRuleRecordService(IServiceCollection services) : base("api/alarm-rule-record")
    {

    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<AlarmRuleRecordDto>> GetListAsync(IEventBus eventbus, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime, [FromQuery] string sorting = "", [FromQuery] int page = 1, [FromQuery] int pagesize = 10)
    {
        var inputDto = new GetAlarmRuleRecordInputDto(startTime, endTime, sorting, page, pagesize);
        var query = new GetAlarmRuleRecordListQuery(inputDto);
        await eventbus.PublishAsync(query);
        return query.Result;
    }
}
