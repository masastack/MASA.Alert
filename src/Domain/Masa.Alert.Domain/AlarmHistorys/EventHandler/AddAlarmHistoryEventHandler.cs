// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.EventHandler;

public class AddAlarmHistoryEventHandler
{
    private readonly IAlarmHistoryRepository _repository;

    public AddAlarmHistoryEventHandler(IAlarmHistoryRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(AddAlarmHistoryEvent eto)
    {
        var alarm = new AlarmHistory(eto.AlarmRuleId, eto.AlertSeverity, eto.AlarmRuleItems);
        await _repository.AddAsync(alarm);
    }
}
