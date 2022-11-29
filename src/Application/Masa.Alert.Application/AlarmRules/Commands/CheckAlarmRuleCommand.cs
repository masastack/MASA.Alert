// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Commands;

public record CheckAlarmRuleCommand(Guid AlarmRuleId, DateTimeOffset? ExcuteTime) : Command
{
    public AlarmRule AlarmRule { get; set; } = default!;

    public ConcurrentDictionary<string, long> AggregateResult { get; set; } = new();

    public ConcurrentDictionary<string, bool> RulesResult { get; set; } = new();

    public bool IsStop { get; set; }
}
