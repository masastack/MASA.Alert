// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class LogAggregationType : Enumeration
{
    public static LogAggregationType Count = new(1, "count");
    public static LogAggregationType Sum = new(2, "sum");
    public static LogAggregationType Avg = new(3, "avg");

    public LogAggregationType(int id, string name) : base(id, name) { }
}