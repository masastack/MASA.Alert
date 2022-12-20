// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class CheckFrequencyQueryModel
{
    public AlarmCheckFrequencyTypes Type { get; set; }

    public TimeIntervalQueryModel FixedInterval { get; set; } = default!;

    public string CronExpression { get; set; } = string.Empty;
}
