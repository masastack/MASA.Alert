// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.


namespace Masa.Alert.Application.AlarmHistories.Queries;

public class AlarmHistoryQueryHandler
{
    private readonly IAlertQueryContext _context;
    private readonly IAuthClient _authClient;

    public AlarmHistoryQueryHandler(IAlertQueryContext context, IAuthClient authClient)
    {
        _context = context;
        _authClient = authClient;
    }

    [EventHandler]
    public async Task GetAsync(GetAlarmHistoryQuery query)
    {
        var entity = await _context.AlarmHistoryQueries
            .Include(x => x.AlarmRule).ThenInclude(x => x.LogMonitorItems)
            .Include(x => x.AlarmRule).ThenInclude(x => x.MetricMonitorItems)
            .Include(x => x.HandleStatusCommits).FirstOrDefaultAsync(x => x.Id == query.AlarmHistoryId);

        Check.NotNull(entity, "AlarmHistory not found");

        query.Result = entity.Adapt<AlarmHistoryDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmHistoryListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var sorting = CreateSorting(options);
        var resultList = await _context.AlarmHistoryQueries.Include(x => x.AlarmRule).GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = sorting
        });

        var dtos = resultList.Result.Adapt<List<AlarmHistoryDto>>();
        await FillAlarmHistoryDtos(dtos);
        var result = new PaginatedListDto<AlarmHistoryDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private Dictionary<string, bool> CreateSorting(GetAlarmHistoryInputDto options)
    {
        switch (options.SearchType)
        {
            case AlarmHistorySearchTypes.Alarming:
                return new Dictionary<string, bool>
                {
                    [nameof(AlarmHistoryQueryModel.AlertSeverity)] = false,
                    [nameof(AlarmHistoryQueryModel.FirstAlarmTime)] = false
                }; 
            case AlarmHistorySearchTypes.Processed:
                return new Dictionary<string, bool>
                {
                    [nameof(AlarmHistoryQueryModel.RecoveryTime)] = true
                };
            case AlarmHistorySearchTypes.NoNotice:
                return new Dictionary<string, bool>
                {
                    [nameof(AlarmHistoryQueryModel.LastAlarmTime)] = true
                };
            default:
                return new Dictionary<string, bool>
                {
                    [nameof(AlarmHistoryQueryModel.ModificationTime)] = true
                };
        }
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
                condition = condition.And(x => x.HandleStatus >= AlarmHistoryHandleStatuses.ProcessingCompleted);
                break;
            case AlarmHistorySearchTypes.NoNotice:
                condition = condition.And(x => !x.IsNotification);
                break;
            default:
                break;
        }
        if (options.TimeType == AlarmHistorySearchTimeTypes.FirstAlarmTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.FirstAlarmTime >= options.StartTime)
                .And(options.EndTime.HasValue, x => x.FirstAlarmTime <= options.EndTime);
        }
        if (options.TimeType == AlarmHistorySearchTimeTypes.LastAlarmTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.LastAlarmTime >= options.StartTime)
                .And(options.EndTime.HasValue, x => x.LastAlarmTime <= options.EndTime);
        }
        condition = condition.And(options.AlertSeverity != default, x => x.AlertSeverity == options.AlertSeverity);
        condition = condition.And(options.HandleStatus != default, x => x.HandleStatus == options.HandleStatus);
        condition = condition.And(options.AlarmRuleId.HasValue, x => x.AlarmRuleId == options.AlarmRuleId);
        condition = condition.And(options.Handler != default, x => x.Handler == options.Handler);
        return await Task.FromResult(condition); ;
    }

    private async Task FillAlarmHistoryDtos(List<AlarmHistoryDto> dtos)
    {
        var userIds = dtos.Where(x => x.Handle.Handler != default).Select(x => x.Handle.Handler).Distinct().ToArray();
        var userInfos = await _authClient.UserService.GetUsersAsync(userIds);
        foreach (var item in dtos)
        {
            item.Handle.HandlerName = userInfos.FirstOrDefault(x => x.Id == item.Handle.Handler)?.StaffDislpayName ?? string.Empty;
        }
    }
}
