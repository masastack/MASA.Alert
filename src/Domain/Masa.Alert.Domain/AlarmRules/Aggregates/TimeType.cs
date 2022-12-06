// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class TimeType : Enumeration
{
    public static TimeType Minute = new MinuteTime();

    public static TimeType Hour = new HourTime();

    public static TimeType Day = new DayTime();

    public TimeType(int id, string name) : base(id, name)
    {
    }

    public virtual TimeSpan GetIntervalTime(int intervalTime)
    {
        throw new NotImplementedException();
    }

    public virtual string GetCronExpression(int intervalTime)
    {
        throw new NotImplementedException();
    }


    private class MinuteTime : TimeType
    {
        public MinuteTime() : base(1, nameof(Minute)) { }

        public override TimeSpan GetIntervalTime(int intervalTime)
        {
            return TimeSpan.FromMinutes(intervalTime);
        }

        public override string GetCronExpression(int intervalTime) {
            return $"0 0/{intervalTime} * ? * * ";
        }
    }

    private class HourTime : TimeType
    {
        public HourTime() : base(2, nameof(Hour)) { }

        public override TimeSpan GetIntervalTime(int intervalTime)
        {
            return TimeSpan.FromHours(intervalTime);
        }

        public override string GetCronExpression(int intervalTime)
        {
            return $"0 0 0/{intervalTime} ? * * ";
        }
    }

    private class DayTime : TimeType
    {
        public DayTime() : base(3, nameof(Day)) { }

        public override TimeSpan GetIntervalTime(int intervalTime)
        {
            return TimeSpan.FromDays(intervalTime);
        }

        public override string GetCronExpression(int intervalTime)
        {
            return $"0 0 0 1/{intervalTime} * ? ";
        }
    }
}
