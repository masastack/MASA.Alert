// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmRules;

public class MetricAggregationTypes : Enumeration
{
    public static MetricAggregationTypes Count = new(1, "Count");
    public static MetricAggregationTypes Sum = new(2, "Sum");
    public static MetricAggregationTypes Avg = new(3, "Avg");

    public MetricAggregationTypes(int id, string name) : base(id, name) { }
}