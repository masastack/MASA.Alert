// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistorys.Events;

public record AddAlarmHistoryEvent(Guid AlarmRuleId, AlertSeverity AlertSeverity, List<AlarmRuleItem> AlarmRuleItems) : DomainEvent
{
}
