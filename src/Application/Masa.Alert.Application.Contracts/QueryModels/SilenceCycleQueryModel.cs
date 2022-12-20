// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.QueryModels;

public class SilenceCycleQueryModel
{
    public SilenceCycleTypes Type { get; set; }

    public TimeIntervalQueryModel TimeInterval { get; set; } = default!;

    public int SilenceCycleValue { get; set; }
}
