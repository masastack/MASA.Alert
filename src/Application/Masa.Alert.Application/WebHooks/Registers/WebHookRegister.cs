// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.Registers;

public class WebHookRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<WebHook, WebHookDto>().MapToConstructor(true);
        config.ForType<WebHookUpsertDto, WebHook>().MapToConstructor(true);
    }
}