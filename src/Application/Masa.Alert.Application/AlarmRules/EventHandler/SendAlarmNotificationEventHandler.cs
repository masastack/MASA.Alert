// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmNotificationEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRuleRepository;
    private readonly ITscClient _tscClient;
    private readonly AlarmRuleDomainService _domainService;

    public SendAlarmNotificationEventHandler(INotificationSender notificationSender
        , IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRuleRepository
        , ITscClient tscClient
        , AlarmRuleDomainService domainService)
    {
        _notificationSender = notificationSender;
        _repository = repository;
        _alarmRuleRepository = alarmRuleRepository;
        _tscClient = tscClient;
        _domainService = domainService;
    }

    [EventHandler]
    public async Task HandleEventAsync(SendAlarmNotificationEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmHistoryId);
        if (alarm == null) return;

        foreach (var item in alarm.RuleResultItems)
        {
            if (!item.IsValid || !item.AlarmRuleItem.IsNotification) continue;

            var notificationConfig = item.AlarmRuleItem.NotificationConfig;

            var variables = new Dictionary<string, object>();
            var alarmRule = await _alarmRuleRepository.FindAsync(x => x.Id == eto.AlarmRuleId);
            if (alarmRule != null)
            {
                variables.TryAdd(AlertConsts.ALARM_RULE_NAME_NOTIFICATION_TEMPLATE_VAR_NAME, alarmRule.DisplayName);

                if (alarmRule.Type == AlarmRuleTypes.Log)
                {
                    await AddLogVariablesAsync(alarmRule, variables);
                }
            }

            await _notificationSender.SendAsync(notificationConfig, variables);
        }

        alarm.Notification();
        await _repository.UpdateAsync(alarm);
    }

    private async Task AddLogVariablesAsync(AlarmRule alarmRule, Dictionary<string, object> variables)
    {
        var checkTime = DateTimeOffset.Now;
        var latest = await _domainService.GetLatest(alarmRule.Id);
        var startTime = alarmRule.GetStartCheckTime(checkTime, latest);
        if (startTime == null)
            return;

        var request = new LogLatestRequest
        {
            Query = alarmRule.WhereExpression,
            Start = startTime.Value.UtcDateTime,
            End = checkTime.UtcDateTime,
            IsDesc = true
        };

        var log = await _tscClient.LogService.GetLatestAsync(request);
        if (log == null)
            return;

        var prefix = "Log";
        variables.TryAdd($"{prefix}.{nameof(log.Timestamp)}", log.Timestamp);
        variables.TryAdd($"{prefix}.{nameof(log.TraceId)}", log.TraceId);
        variables.TryAdd($"{prefix}.{nameof(log.SpanId)}", log.SpanId);
        variables.TryAdd($"{prefix}.{nameof(log.TraceFlags)}", log.TraceFlags);
        variables.TryAdd($"{prefix}.{nameof(log.SeverityText)}", log.SeverityText);
        variables.TryAdd($"{prefix}.{nameof(log.SeverityNumber)}", log.SeverityNumber);
        variables.TryAdd($"{prefix}.{nameof(log.Body)}", log.Body);

        foreach (var resource in log.Resource)
        {
            variables.TryAdd($"{prefix}.{nameof(log.Resource)}.{resource.Key}", resource.Value);
        }

        foreach (var attribute in log.Attributes)
        {
            variables.TryAdd($"{prefix}.{nameof(log.Attributes)}.{attribute.Key}", attribute.Value);
        }
    }
}
