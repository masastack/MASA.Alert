// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class SilenceCycleViewModel
{
    public SilenceCycleTypes Type { get; set; }

    public TimeIntervalViewModel TimeInterval { get; set; } = new();

    public int SilenceCycleValue { get; set; }
}
