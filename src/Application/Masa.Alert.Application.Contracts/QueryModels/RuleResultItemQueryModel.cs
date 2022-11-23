// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class RuleResultItemQueryModel
{
    public bool IsValid { get; set; }

    public AlarmRuleItemQueryModel AlarmRuleItem { get; set; } = new();
}
