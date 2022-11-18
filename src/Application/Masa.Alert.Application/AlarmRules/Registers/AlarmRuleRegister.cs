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
    }
}