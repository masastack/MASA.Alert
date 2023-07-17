// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmHistories.Queries;

public class AlarmHistoryQueryHandler
{
    private readonly IAlertQueryContext _context;
    private readonly IAuthClient _authClient;
    private readonly IDataFilter _dataFilter;
    private readonly II18n<DefaultResource> _i18n;

    public AlarmHistoryQueryHandler(IAlertQueryContext context, IAuthClient authClient, IDataFilter dataFilter, II18n<DefaultResource> i18n)
    {
        _context = context;
        _authClient = authClient;
        _dataFilter = dataFilter;
        _i18n = i18n;
    }

    [EventHandler]
    public async Task GetAsync(GetAlarmHistoryQuery query)
    {
        using var dataFilter = _dataFilter.Disable<ISoftDelete>();
        var entity = await _context.AlarmHistoryQueries
            .Include(x => x.AlarmRule).ThenInclude(x => x.LogMonitorItems)
            .Include(x => x.AlarmRule).ThenInclude(x => x.MetricMonitorItems)
            .Include(x => x.HandleStatusCommits).FirstOrDefaultAsync(x => x.Id == query.AlarmHistoryId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmHistory"));

        query.Result = entity.Adapt<AlarmHistoryDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmHistoryListQuery query)
    {
        using var dataFilter = _dataFilter.Disable<ISoftDelete>();
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var sorting = options.ApplySorting();
        var resultList = await _context.AlarmHistoryQueries.Include(x => x.AlarmRule).Where(x => !x.IsDeleted).GetPaginatedListAsync(condition, new()
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

    private async Task<Expression<Func<AlarmHistoryQueryModel, bool>>> CreateFilteredPredicate(GetAlarmHistoryInputDto options)
    {
        Expression<Func<AlarmHistoryQueryModel, bool>> condition = x => true;
        condition = condition.And(!string.IsNullOrEmpty(options.Filter), x => x.AlarmRule.DisplayName.Contains(options.Filter));
        switch (options.SearchType)
        {
            case AlarmHistorySearchTypes.Alarming:
                condition = condition.And(x => !x.RecoveryTime.HasValue && x.IsNotification && x.HandleStatus != AlarmHistoryHandleStatuses.ProcessingCompleted);
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
        if (options.TimeType == AlarmHistorySearchTimeTypes.ProcessingCompletedTime)
        {
            condition = condition.And(x => x.HandleStatus >= AlarmHistoryHandleStatuses.ProcessingCompleted)
                .And(options.StartTime.HasValue, x => x.RecoveryTime >= options.StartTime)
                .And(options.EndTime.HasValue, x => x.RecoveryTime <= options.EndTime);
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
        var userInfos = await _authClient.UserService.GetListByIdsAsync(userIds);
        foreach (var item in dtos)
        {
            var handler = userInfos.FirstOrDefault(x => x.Id == item.Handle.Handler);
            item.Handle.HandlerName = handler?.RealDisplayName ?? string.Empty;
        }
    }
}
