// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class UpsertAlarmRuleJobEventHandler
{
    private readonly ISchedulerClient _schedulerClient;
    private readonly IAlarmRuleRepository _repository;
    private readonly IMasaStackConfig _masaStackConfig;
    private readonly ILogger<UpsertAlarmRuleJobEventHandler> _logger;
    private readonly IUserContext _userContext;

    public UpsertAlarmRuleJobEventHandler(ISchedulerClient schedulerClient
        , IAlarmRuleRepository repository
        , IMasaStackConfig masaStackConfig
        , ILogger<UpsertAlarmRuleJobEventHandler> logger
        , IUserContext userContext)
    {
        _schedulerClient = schedulerClient;
        _repository = repository;
        _logger = logger;
        _masaStackConfig = masaStackConfig;
        _userContext = userContext;
    }

    [EventHandler]
    public async Task HandleEventAsync(UpsertAlarmRuleJobEvent eto)
    {
        var alarmRule = await _repository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule == null) return;
        var alertUrl = _masaStackConfig.GetAlertServiceDomain();
        var request = new AddSchedulerJobRequest
        {
            ProjectIdentity = MasaStackConsts.ALERT_SYSTEM_ID,
            Name = alarmRule.DisplayName,
            JobType = JobTypes.Http,
            CronExpression = alarmRule.GetCronExpression(),
            OperatorId = _userContext.GetUserId<Guid>(),
            HttpConfig = new SchedulerJobHttpConfig
            {
                HttpMethod = HttpMethods.POST,
                RequestUrl = $"{alertUrl}/api/v1/AlarmRules/{alarmRule.Id}/check"
            }
        };

        if (alarmRule.SchedulerJobId != default)
        {
            try
            {
                await _schedulerClient.SchedulerJobService.RemoveAsync(new SchedulerJobRequestBase
                {
                    JobId = alarmRule.SchedulerJobId,
                    OperatorId = alarmRule.Modifier
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AlarmRuleCheckJob");
            }
        }

        var jobId = await _schedulerClient.SchedulerJobService.AddAsync(request);
        alarmRule.SetSchedulerJobId(jobId);
        await _repository.UpdateAsync(alarmRule);

        if (!alarmRule.IsEnabled)
        {
            await _schedulerClient.SchedulerJobService.DisableAsync(new SchedulerJobRequestBase
            {
                JobId = alarmRule.SchedulerJobId,
                OperatorId = alarmRule.Modifier
            });
        }
    }
}
