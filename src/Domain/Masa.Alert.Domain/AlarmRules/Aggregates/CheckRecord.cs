// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class CheckRecord : ValueObject
{
    public bool IsTrigger { get; protected set; }

    public int ConsecutiveCount { get; protected set; }

    public DateTimeOffset ExcuteTime { get; protected set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; protected set; } = new();

    public List<RuleResultItem> RuleResultItems { get; protected set; } = new();

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return IsTrigger;
        yield return ConsecutiveCount;
        yield return ExcuteTime;
        yield return AggregateResult;
        yield return RuleResultItems;
    }
}
