// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Events;

public record UpdateAlarmRuleRecordAlarmIdEvent(Guid AlarmRuleId, Guid AlarmHistoryId) : DomainEvent
{
}
