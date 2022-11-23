// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.Shared.AlarmRules;

public enum MetricComparisonOperator
{
    // >
    GreaterThan = 1,
    // >=
    GreaterThanOrEqualTo,
    // <
    LessThan,
    // <=
    LessThanOrEqualTo,
    // ==
    EqualTo,
}
