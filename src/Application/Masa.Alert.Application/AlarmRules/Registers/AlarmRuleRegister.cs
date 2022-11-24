// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Registers;

public class AlarmRuleRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AlarmRule, AlarmRuleDto>().MapToConstructor(true);
        config.ForType<AlarmRuleUpsertDto, AlarmRule>().MapToConstructor(true);
        config.ForType<CheckFrequencyDto, CheckFrequency>().MapToConstructor(true);
        config.ForType<SilenceCycleDto, SilenceCycle>().MapToConstructor(true);
        config.ForType<TimeIntervalDto, TimeInterval>().MapToConstructor(true);
        config.ForType<AlarmRuleQueryModel, AlarmRuleDto>()
            .Map(dest => dest.CheckFrequency.FixedInterval.IntervalTime, src => src.CheckFrequencyIntervalTime)
            .Map(dest => dest.CheckFrequency.FixedInterval.IntervalTimeType, src => src.CheckFrequencyIntervalTimeType)
            .Map(dest => dest.CheckFrequency.CronExpression, src => src.CheckFrequencyCron)
            .Map(dest => dest.CheckFrequency.Type, src => src.CheckFrequencyType);
        config.ForType<MetricMonitorItem, MetricMonitorItemDto>().MapToConstructor(true);
        config.ForType<MetricAggregation, MetricAggregationDto>().MapToConstructor(true)
            .Map(dest => dest.ComparisonOperator, src => src.ComparisonOperator.Id)
            .Map(dest => dest.AggregationType, src => src.AggregationType.Id);
        //config.ForType<MetricMonitorItemDto, MetricMonitorItem>().MapToConstructor(true);
        config.ForType<MetricAggregationDto, MetricAggregation>().MapToConstructor(true)
            .Map(dest => dest.ComparisonOperator, src => Enumeration.FromValue<MetricComparisonOperator>(src.ComparisonOperator))
            .Map(dest => dest.AggregationType, src => Enumeration.FromValue<MetricAggregationTypes>(src.AggregationType));
        //config.ForType<MetricComparisonOperator, int>().Map(dest => dest, src => src.Id);
        //config.ForType<MetricMonitorItemDto, MetricMonitorItem>().ConstructUsing(src=>new MetricMonitorItem(src.IsExpression, src.Expression, new MetricAggregation(src.Name, src.Tag, Enumeration.FromValue<MetricComparisonOperator>(src.ComparisonOperator), src.Value, Enumeration.FromValue<MetricAggregationTypes>(src.AggregationType)), src.Alias, src.IsOffset, src.OffsetPeriod));
        //.Map(dest => dest.Aggregation, src => new MetricAggregation(src.Name, src.Tag, src.ComparisonOperator, src.Value, src.AggregationType));
        //.Map(dest => dest.Aggregation.Name, src => src.Name)
        //.Map(dest => dest.Aggregation.Tag, src => src.Tag)
        //.Map(dest => dest.Aggregation.ComparisonOperator.Id, src => src.ComparisonOperator)
        //.Map(dest => dest.Aggregation.Value, src => src.Value)
        //.Map(dest => dest.Aggregation.AggregationType.Id, src => src.AggregationType);
        //config.ForType<MetricMonitorItemDto, MetricAggregation>().MapToConstructor(true);
    }
}