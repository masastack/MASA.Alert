// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Domain.AlarmHistories.Repositories;

public interface IAlarmHistoryRepository : IRepository<AlarmHistory>
{
    Task<IQueryable<AlarmHistory>> GetQueryableAsync();

    Task<AlarmHistory?> GetLastAsync(Guid alarmRuleId);
}