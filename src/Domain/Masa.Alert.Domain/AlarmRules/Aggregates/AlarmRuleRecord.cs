// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class AlarmRuleRecord : FullEntity<Guid, Guid>
{
    public Guid AlarmRuleId { get; protected set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; protected set; } = new();

    public bool IsTrigger { get; protected set; }

    public int ConsecutiveCount { get; protected set; }

    public List<AlarmRuleItem> TriggerRuleItems { get; protected set; } = new();

    public AlarmRuleRecord(Guid alarmRuleId, ConcurrentDictionary<string, long> aggregateResult, bool isTrigger, int consecutiveCount, List<AlarmRuleItem> triggerRuleItems)
    {
        AlarmRuleId = alarmRuleId;
        AggregateResult = aggregateResult;
        IsTrigger = isTrigger;
        ConsecutiveCount = consecutiveCount;
        TriggerRuleItems = triggerRuleItems;
    }
}
