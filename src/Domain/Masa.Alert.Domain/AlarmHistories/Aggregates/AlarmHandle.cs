// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.Aggregates;

public class AlarmHandle : ValueObject
{
    public bool IsThirdParty { get; protected set; }

    public Guid Handler { get; protected set; }

    public Guid WebHookId { get; protected set; }

    public bool IsHandleNotice { get; protected set; }

    public NotificationConfig NotificationConfig { get; protected set; } = new();

    public AlarmHandle(bool isThirdParty, Guid handler, Guid webHookId, bool isHandleNotice, NotificationConfig notificationConfig)
    {
        IsThirdParty = isThirdParty;
        Handler = handler;
        WebHookId = webHookId;
        IsHandleNotice = isHandleNotice;
        NotificationConfig = notificationConfig;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return IsHandleNotice;
        yield return NotificationConfig;
    }
}