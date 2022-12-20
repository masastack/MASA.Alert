// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Registers;

public class AlarmRuleRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AlarmRuleUpsertDto, AlarmRule>().MapToConstructor(true)
            .Ignore(dest => dest.IsEnabled)
            .Ignore(dest => dest.CheckFrequency);
        config.ForType<CheckFrequencyDto, CheckFrequency>().MapToConstructor(true);
        config.ForType<SilenceCycleDto, SilenceCycle>().MapToConstructor(true);
        //config.ForType<AlarmRuleQueryModel, AlarmRuleDto>()
        //    .Map(dest => dest.CheckFrequency.FixedInterval.IntervalTime, src => src.CheckFrequencyIntervalTime)
        //    .Map(dest => dest.CheckFrequency.FixedInterval.IntervalTimeType, src => src.CheckFrequencyIntervalTimeType)
        //    .Map(dest => dest.CheckFrequency.CronExpression, src => src.CheckFrequencyCron)
        //    .Map(dest => dest.CheckFrequency.Type, src => src.CheckFrequencyType);
        config.ForType<LogMonitorItemDto, LogMonitorItem>().MapToConstructor(true)
            .Map(dest => dest.AggregationType, src => Enumeration.FromValue<LogAggregationType>((int)src.AggregationType));
        config.ForType<MetricAggregationDto, MetricAggregation>().MapToConstructor(true)
            .Map(dest => dest.ComparisonOperator, src => Enumeration.FromValue<MetricComparisonOperator>((int)src.ComparisonOperator))
            .Map(dest => dest.AggregationType, src => Enumeration.FromValue<MetricAggregationType>((int)src.AggregationType));
        config.ForType<TimeIntervalDto, TimeInterval>().ConstructUsing(src => new TimeInterval(src.IntervalTime, src.IntervalTimeType.ToString()));
    }
}