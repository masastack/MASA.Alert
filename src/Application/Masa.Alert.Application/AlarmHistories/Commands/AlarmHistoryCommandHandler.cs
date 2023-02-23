// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Commands;

public class AlarmHistoryCommandHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IUserContext _userContext;
    private readonly IAuthClient _authClient;
    private readonly II18n<DefaultResource> _i18n;

    public AlarmHistoryCommandHandler(IAlarmHistoryRepository repository
        , IUserContext userContext
        , IAuthClient authClient
        , II18n<DefaultResource> i18n)
    {
        _repository = repository;
        _userContext = userContext;
        _authClient = authClient;
        _i18n = i18n;
    }

    [EventHandler]
    public async Task HandleAsync(HandleAlarmHistoryCommand command)
    {
        var currentUser = await _authClient.UserService.GetCurrentUserAsync();
        MasaArgumentException.ThrowIfNull(currentUser, _i18n.T("CurrentUser"));

        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);
        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));

        if (command.AlarmHandle.WebHookId == default)
        {
            command.AlarmHandle.Handler = currentUser.Id;
        }

        var handle = command.AlarmHandle.Adapt<AlarmHandle>();

        var remark = string.Empty;

        if (handle.WebHookId == default)
        {
            remark = currentUser.StaffDislpayName;
        }
        else
        {
            var handlerDisplayName = (await _authClient.UserService.FindByIdAsync(handle.Handler))?.StaffDislpayName ?? string.Empty;
            remark = $"{currentUser.StaffDislpayName}分配处理人:{handlerDisplayName}";
        }

        entity.HandleAlarm(handle, currentUser.Id, remark);

        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteAlarmHistoryCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));

        await _repository.RemoveAsync(entity);
    }

    [EventHandler]
    public async Task HandleCompletedAsync(HandleCompletedAlarmHistoryCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));
        var user = await _authClient.UserService.FindByIdAsync(entity.Handle.Handler);
        var handlerDisplayName = user?.StaffDislpayName ?? string.Empty;
        entity.Completed(entity.Handle.Handler, handlerDisplayName);

        await _repository.UpdateAsync(entity);
    }
}
