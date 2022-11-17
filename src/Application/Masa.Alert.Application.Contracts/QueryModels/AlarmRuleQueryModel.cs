// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleQueryModel
{
    public string DisplayName { get; protected set; } = string.Empty;

    public AlarmRuleTypes AlarmRuleType { get; set; }

    public string ProjectIdentity { get; protected set; } = string.Empty;

    public string AppIdentity { get; protected set; } = string.Empty;
}
