﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Events;

public record SendAlarmNotificationEvent(Guid AlarmHistoryId, Guid AlarmRuleId) : DomainEvent
{
}