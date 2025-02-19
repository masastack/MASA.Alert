﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class AlarmRuleService : ServiceBase
{
    public AlarmRuleService(IServiceCollection services) : base()
    {
        RouteHandlerBuilder = builder =>
        {
            builder.RequireAuthorization();
        };
    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<AlarmRuleDto>> GetListAsync(IEventBus eventbus, GetAlarmRuleInputDto inputDto)
    {
        var query = new GetAlarmRuleListQuery(inputDto);
        await eventbus.PublishAsync(query);
        return query.Result;
    }

    public async Task<AlarmRuleDto> GetAsync(IEventBus eventBus, Guid id)
    {
        var query = new GetAlarmRuleQuery(id);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task<Guid> CreateAsync(IEventBus eventBus, [FromBody] AlarmRuleUpsertDto inputDto)
    {
        var command = new CreateAlarmRuleCommand(inputDto);
        await eventBus.PublishAsync(command);
        return command.Result;
    }

    public async Task UpdateAsync(IEventBus eventBus, Guid id, [FromBody] AlarmRuleUpsertDto inputDto)
    {
        var command = new UpdateAlarmRuleCommand(id, inputDto);
        await eventBus.PublishAsync(command);
    }

    public async Task DeleteAsync(IEventBus eventBus, Guid id)
    {
        var command = new DeleteAlarmRuleCommand(id);
        await eventBus.PublishAsync(command);
    }

    [RoutePattern("{id}/enabled/{isEnabled}", StartWithBaseUri = true, HttpMethod = "Put")]
    public async Task SetIsEnabledAsync(IEventBus eventBus, Guid id, bool isEnabled)
    {
        if (isEnabled)
        {
            var command = new EnabledAlarmRuleCommand(id);
            await eventBus.PublishAsync(command);
        }
        else
        {
            var command = new DisableAlarmRuleCommand(id);
            await eventBus.PublishAsync(command);
        }
    }

    [AllowAnonymous]
    [RoutePattern("{id}/check", StartWithBaseUri = true, HttpMethod = "Post")]
    public async Task CheckAsync(Guid id, DateTimeOffset? excuteTime)
    {
        var args = new CheckAlarmRuleJobArgs()
        {
            AlarmRuleId = id,
            ExcuteTime = excuteTime,
            TraceParent = Activity.Current?.Id
        };
        await BackgroundJobManager.EnqueueAsync(args);
    }
}
