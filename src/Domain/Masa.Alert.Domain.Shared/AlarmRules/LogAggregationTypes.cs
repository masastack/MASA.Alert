// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmRules;

public class LogAggregationTypes : Enumeration
{
    public static LogAggregationTypes Count = new(1, "count");
    public static LogAggregationTypes Sum = new(2, "sum");
    public static LogAggregationTypes Avg = new(3, "avg");

    public LogAggregationTypes(int id, string name) : base(id, name) { }
}