// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Commands;

public class AlarmHistoryCommandHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IUserContext _userContext;
    private readonly IAuthClient _authClient;
    private readonly II18n<DefaultResource> _i18n;
    private readonly IDistributedCacheClient _cacheClient;

    public AlarmHistoryCommandHandler(IAlarmHistoryRepository repository
        , IUserContext userContext
        , IAuthClient authClient
        , II18n<DefaultResource> i18n
        , IDistributedCacheClient cacheClient)
    {
        _repository = repository;
        _userContext = userContext;
        _authClient = authClient;
        _i18n = i18n;
        _cacheClient = cacheClient;
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
            remark = currentUser.RealDisplayName;
        }
        else
        {
            var handlerUser = await _authClient.UserService.GetByIdAsync(handle.Handler);
            var handlerDisplayName = handlerUser?.RealDisplayName;
            remark = $"{currentUser.RealDisplayName}{_i18n.T("AllocationProcessor")}:{handlerDisplayName}";
        }

        entity.HandleAlarm(handle, currentUser.Id, remark);

        await _repository.UpdateAsync(entity);

        if (handle.WebHookId == default)
        {
            var cacheKey = $"{AlarmCacheKeys.ALARM_CONSECUTIVE_COUNT}_{entity.AlarmRuleId}";
            await _cacheClient.RemoveAsync<long>(cacheKey);
        }
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteAlarmHistoryCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));

        await _repository.RemoveAsync(entity);
    }

    [EventHandler]
    public async Task ChangeHandlerAsync(ChangeHandlerAlarmHistoryCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);
        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));

        if (entity.Handle.Status != AlarmHistoryHandleStatuses.InProcess)
        {
            throw new UserFriendlyException(errorCode: UserFriendlyExceptionCodes.POST_WEB_HOOK_FAIL);
        }

        var handlerUser = await _authClient.UserService.GetByIdAsync(command.Handler);
        MasaArgumentException.ThrowIfNull(handlerUser, _i18n.T("HandlerNotExist"));

        string remark = $"{_i18n.T("AllocationProcessor")}:{handlerUser.RealDisplayName}";
        entity.ChangeHandler(command.Handler, remark);
        await _repository.UpdateAsync(entity);
    }

    [EventHandler]
    public async Task CompletedAsync(HandleCompletedAlarmHistoryCommand command)
    {
        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));
        var user = await _authClient.UserService.GetByIdAsync(entity.Handle.Handler);
        var handlerDisplayName = user?.RealDisplayName ?? string.Empty;
        entity.Completed(entity.Handle.Handler, handlerDisplayName);

        await _repository.UpdateAsync(entity);
    }
}
