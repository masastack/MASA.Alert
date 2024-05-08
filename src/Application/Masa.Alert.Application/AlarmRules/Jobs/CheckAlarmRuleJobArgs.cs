// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Jobs;

public class CheckAlarmRuleJobArgs
{
    public Guid AlarmRuleId {  get; set; }

    public DateTimeOffset? ExcuteTime { get; set; }

    public string? TraceParent { get; set; }
}
