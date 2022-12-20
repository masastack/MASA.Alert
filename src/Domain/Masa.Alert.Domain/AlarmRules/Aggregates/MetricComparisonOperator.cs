// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules;

public class MetricComparisonOperator : Enumeration
{
    public static MetricComparisonOperator GreaterThan = new(1, ">");
    public static MetricComparisonOperator GreaterOrEqual = new(2, ">=");
    public static MetricComparisonOperator LessThan = new(3, "<");
    public static MetricComparisonOperator LessOrEqual = new(4, "<=");
    public static MetricComparisonOperator Equal = new(5, "=");
    public static MetricComparisonOperator NotEqual = new (6, "!=");

    public MetricComparisonOperator(int id, string name) : base(id, name) { }
}