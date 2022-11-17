// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Registers;

public class AlarmHistoryRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AlarmHistoryDto, AlarmHistoryListViewModel>().Map(dest => dest.DisplayName, src => src.AlarmRule.DisplayName);
    }
}