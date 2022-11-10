// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.Repositories;

public class AlarmRuleRepository : Repository<AlertDbContext, AlarmRule>, IAlarmRuleRepository
{
    public AlarmRuleRepository(AlertDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {

    }

    private async Task<IQueryable<AlarmRule>> GetQueryableAsync()
    {
        return await Task.FromResult(Context.Set<AlarmRule>().AsQueryable());
    }

    public async Task<IQueryable<AlarmRule>> WithDetailsAsync()
    {
        var query = await GetQueryableAsync();
        return query.IncludeDetails();
    }
}
