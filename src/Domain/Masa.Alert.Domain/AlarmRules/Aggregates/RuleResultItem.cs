// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class RuleResultItem : ValueObject
{
    public bool IsValid { get; set; }

    public AlarmRuleItem AlarmRuleItem { get; set; }

    public RuleResultItem(bool isValid, AlarmRuleItem alarmRuleItem)
    {
        IsValid = isValid;
        AlarmRuleItem = alarmRuleItem;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return IsValid;
        yield return AlarmRuleItem;
    }
}
