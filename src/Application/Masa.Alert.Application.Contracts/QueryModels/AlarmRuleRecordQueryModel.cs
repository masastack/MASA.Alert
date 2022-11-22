// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleRecordQueryModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; set; } = new();

    public bool IsTrigger { get; set; }

    public int ConsecutiveCount { get; set; }

    public DateTime ModificationTime { get; set; }

    public DateTime CreationTime { get; set; }

    public List<RuleResultItemQueryModel> RuleResultItems { get; set; } = new();
}
