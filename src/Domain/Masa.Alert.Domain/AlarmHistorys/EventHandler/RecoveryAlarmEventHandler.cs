// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.EventHandler;

public class RecoveryAlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;

    public RecoveryAlarmEventHandler(IAlarmHistoryRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(RecoveryAlarmEvent eto)
    {
        var queryable = await _repository.GetQueryableAsync();
        var alarm = queryable.Where(x => x.AlarmRuleId == eto.AlarmRuleId).OrderByDescending(x => x.CreationTime).FirstOrDefault();

        if (alarm == null) return;

        alarm.Recovery();
        await _repository.UpdateAsync(alarm);
    }
}
