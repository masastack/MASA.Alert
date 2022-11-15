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
    }
}