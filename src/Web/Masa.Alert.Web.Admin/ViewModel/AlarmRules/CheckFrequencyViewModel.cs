// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class CheckFrequencyViewModel
{
    public AlarmCheckFrequencyTypes Type { get; set; }

    public TimeIntervalViewModel FixedInterval { get; set; } = new();

    public string CronExpression { get; set; } = string.Empty;
}
