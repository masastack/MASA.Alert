// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.Repositories;

public class AlarmRuleRecordRepository : Repository<AlertDbContext, AlarmRuleRecord>, IAlarmRuleRecordRepository
{
    public AlarmRuleRecordRepository(AlertDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {

    }

    public async Task<IQueryable<AlarmRuleRecord>> GetQueryableAsync()
    {
        return await Task.FromResult(Context.Set<AlarmRuleRecord>().AsQueryable());
    }
}
