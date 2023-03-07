﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class AlarmRuleRecordService : ServiceBase
{
    public AlarmRuleRecordService(IServiceCollection services) : base()
    {

    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<AlarmRuleRecordDto>> GetListAsync(IEventBus eventbus, GetAlarmRuleRecordInputDto inputDto)
    {
        var query = new GetAlarmRuleRecordListQuery(inputDto);
        await eventbus.PublishAsync(query);
        return query.Result;
    }
}
