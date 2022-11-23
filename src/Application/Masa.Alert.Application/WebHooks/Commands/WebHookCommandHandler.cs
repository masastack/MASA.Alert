// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.Commands;

public class WebHookCommandHandler
{
    private readonly IWebHookRepository _repository;
    private readonly AlarmRuleDomainService _domainService;
    private readonly IDomainEventBus _domainEventBus;

    public WebHookCommandHandler(IWebHookRepository repository
        , AlarmRuleDomainService domainService
        , IDomainEventBus domainEventBus)
    {
        _repository = repository;
        _domainService = domainService;
        _domainEventBus = domainEventBus;
    }

    [EventHandler]
    public async Task CreateAsync(CreateWebHookCommand command)
    {
        var entity = command.WebHook.Adapt<WebHook>();
        await _repository.AddAsync(entity);
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateWebHookCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.WebHookId);

        Check.NotNull(entity, "WebHook not found");

        command.WebHook.Adapt(entity);

        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteWebHookCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.WebHookId);

        Check.NotNull(entity, "WebHook not found");

        await _repository.RemoveAsync(entity);
    }

    public async Task TestAsync(TestWebHookCommand command)
    {
        await _domainEventBus.PublishAsync(new PostWebHookEvent(command.WebHookId, command.Handler));
    }
}
