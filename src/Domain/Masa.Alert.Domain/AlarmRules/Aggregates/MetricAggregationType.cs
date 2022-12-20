// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class MetricAggregationType : Enumeration
{
    public static MetricAggregationType Count = new(1, "Count");
    public static MetricAggregationType Sum = new(2, "Sum");
    public static MetricAggregationType Avg = new(3, "Avg");

    public MetricAggregationType(int id, string name) : base(id, name) { }
}