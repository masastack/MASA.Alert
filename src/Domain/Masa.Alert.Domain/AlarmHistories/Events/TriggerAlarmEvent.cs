// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Events;

public record TriggerAlarmEvent(Guid AlarmRuleId, AlertSeverity AlertSeverity, List<RuleResultItem> TriggerRuleItems
    , DateTimeOffset ExcuteTime, ConcurrentDictionary<string, long> AggregateResult, int ConsecutiveCount) : DomainEvent
{
}
