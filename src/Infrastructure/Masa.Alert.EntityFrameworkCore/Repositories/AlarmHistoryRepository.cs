// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.Repositories;

public class AlarmHistoryRepository : Repository<AlertDbContext, AlarmHistory>, IAlarmHistoryRepository
{
    public AlarmHistoryRepository(AlertDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {

    }

    public async Task<IQueryable<AlarmHistory>> GetQueryableAsync()
    {
        return await Task.FromResult(Context.Set<AlarmHistory>().AsQueryable());
    }

    public async Task<AlarmHistory?> GetLastAsync(Guid alarmRuleId)
    {
        return await Context.Set<AlarmHistory>().Where(x=>x.AlarmRuleId== alarmRuleId).OrderByDescending(x=>x.CreationTime).FirstOrDefaultAsync();
    }
}
