﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Repositories;

public interface IAlarmRuleRecordRepository : IRepository<AlarmRuleRecord>
{
    Task<IQueryable<AlarmRuleRecord>> GetQueryableAsync();
}
