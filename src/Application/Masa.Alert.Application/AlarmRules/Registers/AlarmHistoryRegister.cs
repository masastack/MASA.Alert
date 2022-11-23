// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Registers;

public class AlarmHistoryRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AlarmHandleDto, AlarmHandle>().MapToConstructor(true);
        config.ForType<AlarmHistoryQueryModel, AlarmHistoryDto>()
            .Map(dest => dest.Handle.Status, src => src.HandleStatus)
            .Map(dest => dest.Handle.Handler, src => src.Handler)
            .Map(dest => dest.Handle.WebHookId, src => src.WebHookId)
            .Map(dest => dest.Handle.IsHandleNotice, src => src.IsHandleNotice)
            .Map(dest => dest.Handle.NotificationConfig, src => src.HandleNotificationConfig);
    }
}
