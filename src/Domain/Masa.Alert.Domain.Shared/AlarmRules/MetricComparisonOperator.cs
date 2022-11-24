// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmRules;

public class MetricComparisonOperator : Enumeration
{
    public static MetricComparisonOperator GreaterThan = new(1, ">");
    public static MetricComparisonOperator GreaterThanOrEqualTo = new(2, ">=");
    public static MetricComparisonOperator LessThan = new(3, "<");
    public static MetricComparisonOperator LessThanOrEqualTo = new(4, "<=");
    public static MetricComparisonOperator EqualTo = new(5, "=");

    public MetricComparisonOperator(int id, string name) : base(id, name) { }
}