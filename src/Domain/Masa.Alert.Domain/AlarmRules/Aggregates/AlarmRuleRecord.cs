// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class AlarmRuleRecord : FullEntity<Guid, Guid>
{
    public Guid AlarmRuleId { get; protected set; }

    public Guid AlarmHistoryId { get; protected set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; protected set; } = new();

    public bool IsTrigger { get; protected set; }

    public int ConsecutiveCount { get; protected set; }

    public DateTimeOffset ExcuteTime { get; protected set; }

    public List<RuleResultItem> RuleResultItems { get; protected set; } = new();

    public AlarmRuleRecord(Guid alarmRuleId, ConcurrentDictionary<string, long> aggregateResult, bool isTrigger, int consecutiveCount, DateTimeOffset excuteTime, List<RuleResultItem> ruleResultItems, Guid alarmHistoryId)
    {
        AlarmRuleId = alarmRuleId;
        AggregateResult = aggregateResult;
        IsTrigger = isTrigger;
        ConsecutiveCount = consecutiveCount;
        RuleResultItems = ruleResultItems;
        AlarmHistoryId = alarmHistoryId;
        ExcuteTime = excuteTime;
    }

    public void SetAlarmHistoryId(Guid alarmHistoryId)
    {
        AlarmHistoryId = alarmHistoryId;
    }
}
