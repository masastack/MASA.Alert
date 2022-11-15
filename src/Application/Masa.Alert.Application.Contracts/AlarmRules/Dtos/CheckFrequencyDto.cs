// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class CheckFrequencyDto
{
    public AlarmCheckFrequencyTypes Type { get; set; }

    public TimeIntervalDto FixedInterval { get; set; } = new();

    public string CronExpression { get; set; } = string.Empty;
}
