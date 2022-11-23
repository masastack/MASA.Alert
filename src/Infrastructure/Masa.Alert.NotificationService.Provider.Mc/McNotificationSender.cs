// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.NotificationService.Provider.Mc;

public class McNotificationSender : INotificationSender, IScopedDependency
{
    private readonly IMcClient _mcClient;
    private readonly IAuthClient _authClient;

    public McNotificationSender(IMcClient mcClient, IAuthClient authClient)
    {
        _mcClient = mcClient;
        _authClient = authClient;
    }
    public async Task SendAsync(NotificationConfig notificationConfig)
    {
        var receivers = await GeReceivers(notificationConfig);

        if (!receivers.Any())
        {
            return;
        }

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
