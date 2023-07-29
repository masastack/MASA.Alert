// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.EventHandler;

public class NoticeAlarmHandleEventHandler
{
    private readonly INotificationSender _notificationSender;
    private readonly IAlarmRuleRepository _alarmRuleRepository;

    public NoticeAlarmHandleEventHandler(INotificationSender notificationSender, IAlarmRuleRepository alarmRuleRepository)
    {
        _notificationSender = notificationSender;
        _alarmRuleRepository = alarmRuleRepository;
    }

    [EventHandler]
    public async Task HandleEventAsync(NoticeAlarmHandleEvent eto)
    {
        var notificationConfig = eto.AlarmHandle.NotificationConfig;

        var variables = new Dictionary<string, object>();
        var alarmRule = await _alarmRuleRepository.FindAsync(x => x.Id == eto.AlarmRuleId);
        if (alarmRule != null)
        {
            variables.TryAdd(AlertConsts.ALARM_RULE_NAME_NOTIFICATION_TEMPLATE_VAR_NAME, alarmRule.DisplayName);
        }

        await _notificationSender.SendAsync(notificationConfig, variables);
    }
}
