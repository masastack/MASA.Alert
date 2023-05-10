// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Events;

public record AddAlarmRuleRecordEvent(Guid AlarmRuleId, Guid AlarmHistoryId, DateTimeOffset ExcuteTime, ConcurrentDictionary<string, long> AggregateResult, bool IsTrigger, int ConsecutiveCount, List<RuleResultItem> RuleResultItems) : DomainEvent
{
}
