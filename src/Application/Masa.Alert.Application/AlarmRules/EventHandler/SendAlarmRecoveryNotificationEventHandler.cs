// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmRecoveryNotificationEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRuleRepository;

    public SendAlarmRecoveryNotificationEventHandler(INotificationSender notificationSender
        , IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRuleRepository)
    {
        _notificationSender = notificationSender;
        _repository = repository;
        _alarmRuleRepository = alarmRuleRepository;
    }

    [EventHandler]
    public async Task HandleEventAsync(SendAlarmRecoveryNotificationEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmHistoryId);
        if (alarm == null) return;

        foreach (var item in alarm.RuleResultItems)
        {
            if (!item.IsValid || !item.AlarmRuleItem.IsRecoveryNotification) continue;

            var notificationConfig = item.AlarmRuleItem.RecoveryNotificationConfig;

            var variables = new Dictionary<string, object>();
            var alarmRule = await _alarmRuleRepository.FindAsync(x => x.Id == alarm.AlarmRuleId);
            if (alarmRule != null)
            {
                variables.TryAdd(AlertConsts.ALARM_RULE_NAME_NOTIFICATION_TEMPLATE_VAR_NAME, alarmRule.DisplayName);
            }
            await _notificationSender.SendAsync(notificationConfig, variables);
        }
    }
}
