// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.EventHandler;

public class RecoveryAlarmEventHandler
{
    private readonly IAlarmHistoryRepository _repository;
    private readonly IEventBus _eventBus;
    private readonly IDistributedCacheClient _cacheClient;

    public RecoveryAlarmEventHandler(IAlarmHistoryRepository repository
        , IEventBus eventBus
        , IDistributedCacheClient cacheClient)
    {
        _repository = repository;
        _eventBus = eventBus;
        _cacheClient = cacheClient;
    }

    [EventHandler]
    public async Task HandleEventAsync(RecoveryAlarmEvent eto)
    {
        var alarm = await _repository.GetLastAsync(eto.AlarmRuleId);

        if (alarm == null) return;

        alarm.Recovery(true);
        alarm.AddAlarmRuleRecord(eto.ExcuteTime, eto.AggregateResult, false, 0, eto.RuleResultItems);
        await _repository.UpdateAsync(alarm);

        var cacheKey = $"{AlarmCacheKeys.ALARM_CONSECUTIVE_COUNT}_{eto.AlarmRuleId}";
        await _cacheClient.RemoveAsync<long>(cacheKey);
    }
}
