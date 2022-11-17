// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using System.Linq.Expressions;
using Masa.Alert.Domain.Shared.AlarmHistory;

namespace Masa.Alert.Application.AlarmHistorys.Queries;

public class AlarmHistoryQueryHandler
{
    private readonly IAlertQueryContext _context;

    public AlarmHistoryQueryHandler(IAlertQueryContext context)
    {
        _context = context;
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmHistoryListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var resultList = await _context.AlarmHistoryQueries.Include(x => x.AlarmRule).GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(AlarmHistory.ModificationTime)] = true
            }
        });

        var dtos = resultList.Result.Adapt<List<AlarmHistoryDto>>();
        var result = new PaginatedListDto<AlarmHistoryDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<AlarmHistoryQueryModel, bool>>> CreateFilteredPredicate(GetAlarmHistoryInputDto options)
    {
        Expression<Func<AlarmHistoryQueryModel, bool>> condition = x => true;
        condition = condition.And(!string.IsNullOrEmpty(options.Filter), x => x.AlarmRule.DisplayName.Contains(options.Filter));
        switch (options.SearchType)
        {
            case AlarmHistorySearchTypes.Alarming:
                condition = condition.And(x => !x.RecoveryTime.HasValue);
                break;
            case AlarmHistorySearchTypes.Processed:
                condition = condition.And(x => x.Status >= AlarmHistoryStatuses.ProcessingCompleted);
                break;
            case AlarmHistorySearchTypes.NoNotice:
                break;
            default:
                break;
        }
        if (options.TimeType == AlarmHistorySearchTimeTypes.FirstAlarmTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.FirstAlarmTime >= options.StartTime);
            condition = condition.And(options.EndTime.HasValue, x => x.FirstAlarmTime <= options.EndTime);
        }
        if (options.TimeType == AlarmHistorySearchTimeTypes.LastAlarmTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.LastAlarmTime >= options.StartTime);
            condition = condition.And(options.EndTime.HasValue, x => x.LastAlarmTime <= options.EndTime);
        }
        condition = condition.And(options.AlertSeverity != default, x => x.AlertSeverity == options.AlertSeverity);
        condition = condition.And(options.Status != default, x => x.Status == options.Status);
        return await Task.FromResult(condition); ;
    }
}
