// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class AlarmRuleQueryModel
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public AlarmRuleTypes AlarmRuleType { get; set; }

    public string ProjectIdentity { get; set; } = string.Empty;

    public string AppIdentity { get; set; } = string.Empty;
}
