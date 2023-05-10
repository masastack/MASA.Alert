// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.Contracts.AlarmRules.Actors;

public interface IAlarmRuleActor : IActor
{
    Task<bool> Check(DateTimeOffset? excuteTime);
}

