// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmNotificationEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmHistoryRepository _repository;
    private readonly IAlarmRuleRepository _alarmRuleRepository;

    public SendAlarmNotificationEventHandler(INotificationSender notificationSender
        , IAlarmHistoryRepository repository
        , IAlarmRuleRepository alarmRuleRepository)
    {
        _notificationSender = notificationSender;
        _repository = repository;
        _alarmRuleRepository = alarmRuleRepository;
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
            }

            await _notificationSender.SendAsync(notificationConfig, variables);
        }

        alarm.Notification();
        await _repository.UpdateAsync(alarm);
    }
}
