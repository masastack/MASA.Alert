﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Dtos;

public class SilenceCycleDto
{
    public SilenceCycleTypes Type { get; set; }

    public TimeIntervalDto TimeInterval { get; set; } = new();

    public int SilenceCycleValue { get; set; }
}
