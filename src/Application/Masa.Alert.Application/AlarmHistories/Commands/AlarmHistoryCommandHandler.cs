// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Commands;

public class AlarmHistoryCommandHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IUserContext _userContext;
    private readonly IAuthClient _authClient;

    public AlarmHistoryCommandHandler(IAlarmHistoryRepository repository
        , IUserContext userContext
        , IAuthClient authClient)
    {
        _repository = repository;
        _userContext = userContext;
        _authClient = authClient;
    }

    [EventHandler]
    public async Task HandleAsync(HandleAlarmHistoryCommand command)
    {
        var currentUser = await _authClient.UserService.GetCurrentUserAsync();
        Check.NotNull(currentUser, "The current user information is not obtained");

        var entity = await _repository.FindAsync(x => x.Id == command.AlarmHistoryId);
        Check.NotNull(entity, "AlarmHistory not found");

        var handle = command.AlarmHandle.Adapt<AlarmHandle>();

        var remark = string.Empty;

        if (handle.WebHookId == default)
        {
            var handlerDisplayName = (await _authClient.UserService.FindByIdAsync(handle.Handler))?.DisplayName ?? string.Empty;
            remark = $"{currentUser.DisplayName}分配处理人:{handlerDisplayName}";
        }
        else
        {
            remark = currentUser.DisplayName;
        }

        entity.HandleAlarm(handle, currentUser.Id, remark);

        await _repository.UpdateAsync(entity);
    }
}
