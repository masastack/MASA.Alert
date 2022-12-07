// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class UpsertAlarmRuleJobEventHandler
{
    private readonly ISchedulerClient _schedulerClient;
    private readonly IAlarmRuleRepository _repository;
    private readonly IMasaConfiguration _configuration;

    public UpsertAlarmRuleJobEventHandler(ISchedulerClient schedulerClient
        , IAlarmRuleRepository repository
        , IMasaConfiguration configuration)
    {
        _schedulerClient = schedulerClient;
        _repository = repository;
        _configuration = configuration;
    }

    [EventHandler]
    public async Task HandleEventAsync(UpsertAlarmRuleJobEvent eto)
    {
        var alarmRule = await _repository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule == null) return;

        var alertUrl = _configuration.ConfigurationApi.GetPublic().GetValue<string>("AppSettings:AlertClient:Url");
        var request = new AddSchedulerJobRequest
        {
            ProjectIdentity = MasaStackConsts.ALERT_SYSTEM_ID,
            Name = alarmRule.DisplayName,
            JobType = JobTypes.Http,
            JobIdentity = "masa-alert-alarmRule-check-job",
            CronExpression = alarmRule.GetCronExpression(),
            OperatorId = alarmRule.Modifier,
            HttpConfig = new SchedulerJobHttpConfig
            {
                HttpMethod = HttpMethods.POST,
                RequestUrl = $"{alertUrl}/api/v1/AlarmRules/{alarmRule.Id}/check"
            }
        };

        if (alarmRule.SchedulerJobId != default)
        {
            await _schedulerClient.SchedulerJobService.RemoveAsync(new SchedulerJobRequestBase
            {
                JobId = alarmRule.SchedulerJobId,
                OperatorId = alarmRule.Modifier
            });
        }

        var jobId = await _schedulerClient.SchedulerJobService.AddAsync(request);
        alarmRule.SetSchedulerJobId(jobId);
        await _repository.UpdateAsync(alarmRule);
    }
}
