// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class AlarmRuleRecordListViewModel
{
    public Guid Id { get; set; }

    public Guid AlarmRuleId { get; set; }

    public ConcurrentDictionary<string, long> AggregateResult { get; set; } = new();

    public bool IsTrigger { get; set; }

    public int ConsecutiveCount { get; set; }

    public List<RuleResultItemViewModel> RuleResultItems { get; set; } = new();

    public DateTime CreationTime { get; set; }
}
