// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public class AlarmRuleQueryHandler
{
    private readonly IAlarmRuleRepository _repository;
    private readonly IAuthClient _authClient;

    public AlarmRuleQueryHandler(IAlarmRuleRepository repository, IAuthClient authClient)
    {
        _repository = repository;
        _authClient = authClient;
    }

    [EventHandler]
    public async Task GetAsync(GetAlarmRuleQuery query)
    {
        var queryable = await _repository.WithDetailsAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id == query.AlarmRuleId);

        Check.NotNull(entity, "AlarmRule not found");

        query.Result = entity.Adapt<AlarmRuleDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmRuleListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var queryable = await _repository.GetQueryableAsync();
        var resultList = await queryable.GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(AlarmRule.ModificationTime)] = true
            }
        });
        var dtos = resultList.Result.Adapt<List<AlarmRuleDto>>();
        await FillAlarmRuleDtos(dtos);
        var result = new PaginatedListDto<AlarmRuleDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<AlarmRule, bool>>> CreateFilteredPredicate(GetAlarmRuleInputDto options)
    {
        Expression<Func<AlarmRule, bool>> condition = x => true;
        condition = condition.And(!string.IsNullOrEmpty(options.Filter), x => x.DisplayName.Contains(options.Filter));
        condition = condition.And(options.AlarmRuleType != default, x => x.AlarmRuleType == options.AlarmRuleType);
        if (options.TimeType == AlarmRuleSearchTimeTypes.ModificationTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.ModificationTime >= options.StartTime);
            condition = condition.And(options.EndTime.HasValue, x => x.ModificationTime <= options.EndTime);
        }
        if (options.TimeType == AlarmRuleSearchTimeTypes.CreationTime)
        {
            condition = condition.And(options.StartTime.HasValue, x => x.CreationTime >= options.StartTime);
            condition = condition.And(options.EndTime.HasValue, x => x.CreationTime <= options.EndTime);
        }
        condition = condition.And(!string.IsNullOrEmpty(options.ProjectIdentity), x => x.ProjectIdentity == options.ProjectIdentity);
        condition = condition.And(!string.IsNullOrEmpty(options.AppIdentity), x => x.AppIdentity == options.AppIdentity);
        return await Task.FromResult(condition); ;
    }

    private async Task FillAlarmRuleDtos(List<AlarmRuleDto> dtos)
    {
        var modifierUserIds = dtos.Where(x => x.Modifier != default).Select(x => x.Modifier).Distinct().ToArray();
        var userInfos = await _authClient.UserService.GetUserPortraitsAsync(modifierUserIds);
        foreach (var item in dtos)
        {
            item.ModifierName = userInfos.FirstOrDefault(x => x.Id == item.Modifier)?.DisplayName ?? string.Empty;
        }
    }
}
