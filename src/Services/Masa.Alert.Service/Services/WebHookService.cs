// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Services;

public class WebHookService : ServiceBase
{
    public WebHookService(IServiceCollection services) : base()
    {
        RouteHandlerBuilder = builder =>
        {
            builder.RequireAuthorization();
        };
    }

    [RoutePattern("", StartWithBaseUri = true, HttpMethod = "Get")]
    public async Task<PaginatedListDto<WebHookDto>> GetListAsync(IEventBus eventbus, GetWebHookInputDto inputDto)
    {
        var query = new GetWebHookListQuery(inputDto);
        await eventbus.PublishAsync(query);
        return query.Result;
    }

    public async Task<WebHookDto> GetAsync(IEventBus eventBus, Guid id)
    {
        var query = new GetWebHookQuery(id);
        await eventBus.PublishAsync(query);
        return query.Result;
    }

    public async Task CreateAsync(IEventBus eventBus, [FromBody] WebHookUpsertDto inputDto)
    {
        var command = new CreateWebHookCommand(inputDto);
        await eventBus.PublishAsync(command);
    }

    public async Task UpdateAsync(IEventBus eventBus, Guid id, [FromBody] WebHookUpsertDto inputDto)
    {
        var command = new UpdateWebHookCommand(id, inputDto);
        await eventBus.PublishAsync(command);
    }

    public async Task DeleteAsync(IEventBus eventBus, Guid id)
    {
        var command = new DeleteWebHookCommand(id);
        await eventBus.PublishAsync(command);
    }

    [RoutePattern("{id}/test", StartWithBaseUri = true, HttpMethod = "Post")]
    public async Task TestAsync(IEventBus eventBus, Guid id, [FromBody] WebHookTestDto inputDto)
    {
        var command = new TestWebHookCommand(id, inputDto.Handler);
        await eventBus.PublishAsync(command);
    }
}
