// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmRules;


public class MetricAggregationTypes : Enumeration
{
    public static MetricAggregationTypes Count = new(1, "count");
    public static MetricAggregationTypes Sum = new(2, "sum");
    public static MetricAggregationTypes Avg = new(3, "avg");

    public MetricAggregationTypes(int id, string name) : base(id, name) { }
}