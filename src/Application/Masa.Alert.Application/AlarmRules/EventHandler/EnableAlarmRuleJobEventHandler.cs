// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class EnableAlarmRuleJobEventHandler
{
    private readonly ISchedulerClient _schedulerClient;
    private readonly IAlarmRuleRepository _repository;

    public EnableAlarmRuleJobEventHandler(ISchedulerClient schedulerClient
        , IAlarmRuleRepository repository)
    {
        _schedulerClient = schedulerClient;
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(EnableAlarmRuleJobEvent eto)
    {
        var alarmRule = await _repository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule == null) return;

        if (alarmRule.SchedulerJobId != default)
        {
            await _schedulerClient.SchedulerJobService.EnableAsync(new SchedulerJobRequestBase
            {
                JobId = alarmRule.SchedulerJobId,
                OperatorId = alarmRule.Modifier
            });
        }
    }
}