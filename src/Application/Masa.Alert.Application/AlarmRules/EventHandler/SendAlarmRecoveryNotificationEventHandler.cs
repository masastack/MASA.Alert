// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.EventHandler;

public class SendAlarmRecoveryNotificationEventHandler
{
    private readonly IMcClient _mcClient;
    private readonly IAuthClient _authClient;
    private readonly IAlarmHistoryRepository _repository;

    public SendAlarmRecoveryNotificationEventHandler(IMcClient mcClient
        , IAuthClient authClient
        , IAlarmHistoryRepository repository)
    {
        _mcClient = mcClient;
        _authClient = authClient;
        _repository = repository;
    }

    [EventHandler]
    public async Task HandleEventAsync(SendAlarmRecoveryNotificationEvent eto)
    {
        var alarm = await _repository.FindAsync(x => x.Id == eto.AlarmHistoryId);
        if (alarm == null) return;

        foreach (var item in alarm.RuleResultItems)
        {
            if (!item.AlarmRuleItem.IsRecoveryNotification) continue;

            var notificationConfig = item.AlarmRuleItem.RecoveryNotificationConfig;
            var receivers = await GeReceivers(notificationConfig);

            var request = new BuildingBlocks.StackSdks.Mc.Model.SendTemplateMessageModel
            {
                ChannelCode = notificationConfig.ChannelCode,
                ChannelType = (ChannelTypes)notificationConfig.ChannelType,
                TemplateCode = notificationConfig.TemplateCode,
                ReceiverType = SendTargets.Assign,
                Receivers = receivers
            };

            await _mcClient.MessageTaskService.SendTemplateMessageAsync(request);
        }
    }

    private async Task<List<MessageTaskReceiverModel>> GeReceivers(NotificationConfig notificationConfig)
    {
        var receivers = new List<MessageTaskReceiverModel>();
        foreach (var item in notificationConfig.Receivers)
        {
            var user = await _authClient.UserService.FindByIdAsync(item);

            if (user == null) continue;

            var receiver = GetReceiverModel(user, notificationConfig.ChannelType);
            receivers.Add(receiver);
        }
        return receivers;
    }

    private MessageTaskReceiverModel GetReceiverModel(UserModel user, int channelType)
    {
        var receiver = new MessageTaskReceiverModel
        {
            Type = MessageTaskReceiverTypes.User
        };

        switch (channelType)
        {
            case (int)ChannelTypes.Sms:
                receiver.PhoneNumber = user.PhoneNumber ?? string.Empty;
                break;
            case (int)ChannelTypes.Email:
                receiver.Email = user.Email ?? string.Empty;
                break;
            case (int)ChannelTypes.WebsiteMessage:
                receiver.SubjectId = user.Id;
                break;
            default:
                break;
        }

        return receiver;
    }
}
