// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public record GetAlarmRuleQuery(Guid AlarmRuleId) : Query<AlarmRuleDto>
{
    public override AlarmRuleDto Result { get; set; } = new();
}
