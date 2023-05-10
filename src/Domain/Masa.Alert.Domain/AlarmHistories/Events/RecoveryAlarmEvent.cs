// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Events;

public record RecoveryAlarmEvent(Guid AlarmRuleId, DateTimeOffset ExcuteTime, ConcurrentDictionary<string, long> AggregateResult, bool IsTrigger, int ConsecutiveCount) : DomainEvent
{
}
