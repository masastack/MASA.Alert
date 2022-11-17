// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmRules.Repositories;

public interface IAlarmRuleRepository : IRepository<AlarmRule>
{
    Task<IQueryable<AlarmRule>> GetQueryableAsync();

    Task<IQueryable<AlarmRule>> WithDetailsAsync();
}
