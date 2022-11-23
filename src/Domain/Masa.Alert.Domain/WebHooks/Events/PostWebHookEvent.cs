// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.


namespace Masa.Alert.Domain.WebHooks.Events;

public record PostWebHookEvent(Guid WebHookId, Guid Handler) : DomainEvent
{
}