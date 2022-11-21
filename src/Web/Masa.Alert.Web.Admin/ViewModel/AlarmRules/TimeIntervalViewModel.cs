// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.ViewModel.AlarmRules;

public class TimeIntervalViewModel
{
    public int IntervalTime { get; set; } = 15;

    public TimeTypes IntervalTimeType { get; set; } = TimeTypes.Minute;

    public TimeSpan? GetIntervalTime()
    {
        switch (IntervalTimeType)
        {
            case TimeTypes.Minute:
                return TimeSpan.FromMinutes(IntervalTime);
            case TimeTypes.Hour:
                return TimeSpan.FromHours(IntervalTime);
            case TimeTypes.Day:
                return TimeSpan.FromDays(IntervalTime);
            default:
                return null;
        }
    }
}
