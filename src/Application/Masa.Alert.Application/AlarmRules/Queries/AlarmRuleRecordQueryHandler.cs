// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public class AlarmRuleRecordQueryHandler
{
    private readonly IAlertQueryContext _context;

    public AlarmRuleRecordQueryHandler(IAlertQueryContext context)
    {
        _context = context;
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmRuleRecordListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var resultList = await _context.AlarmRuleRecordQueries.GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(AlarmRuleRecordQueryModel.ModificationTime)] = true
            }
        });

        var dtos = resultList.Result.Adapt<List<AlarmRuleRecordDto>>();
        var result = new PaginatedListDto<AlarmRuleRecordDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<AlarmRuleRecordQueryModel, bool>>> CreateFilteredPredicate(GetAlarmRuleRecordInputDto options)
    {
        Expression<Func<AlarmRuleRecordQueryModel, bool>> condition = x => x.AlarmHistoryId== options.AlarmHistoryId;
        return await Task.FromResult(condition); ;
    }
}
