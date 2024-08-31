// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.NotificationService.Provider.Mc;

public class McNotificationSender : INotificationSender, IScopedDependency
{
    private readonly IMcClient _mcClient;

    public McNotificationSender(IMcClient mcClient)
    {
        _mcClient = mcClient;
    }
    public async Task SendAsync(NotificationConfig notificationConfig, Dictionary<string, object> variables)
    {
        var channelType = (ChannelTypes)notificationConfig.ChannelType;

        SendTargets? receiverType = notificationConfig.Receivers.Any()
                     ? SendTargets.Assign
                     : channelType == ChannelTypes.WeixinWorkWebhook
                        ? SendTargets.Broadcast
                        : null;

        if (receiverType == null)
        {
            return;
        }

        var request = new SendTemplateMessageByInternalModel
        {
            ChannelCode = notificationConfig.ChannelCode,
            ChannelType = (ChannelTypes)notificationConfig.ChannelType,
            TemplateCode = notificationConfig.TemplateCode,
            ReceiverType = receiverType.Value,
            Receivers = notificationConfig.Receivers.Select(x => new InternalReceiverModel
            {
                Type = MessageTaskReceiverTypes.User,
                SubjectId = x
            }).ToList(),
            Variables = new System.Collections.Concurrent.ExtraPropertyDictionary(variables)
        };

        await _mcClient.MessageTaskService.SendTemplateMessageByInternalAsync(request);
    }
}
