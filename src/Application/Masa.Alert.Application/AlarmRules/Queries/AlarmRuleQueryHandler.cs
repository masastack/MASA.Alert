// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public class AlarmRuleQueryHandler
{
    private readonly IAlarmRuleRepository _repository;

    public AlarmRuleQueryHandler(IAlarmRuleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetAsync(GetAlarmRuleQuery query)
    {
        var entity = await _repository.FindAsync(x => x.Id == query.AlarmRuleId);

        Check.NotNull(entity, "alarmRule not found");

        query.Result = entity.Adapt<AlarmRuleDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmRuleListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var resultList = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(AlarmRule.ModificationTime)] = true
            }
        });
        var dtos = resultList.Result.Adapt<List<AlarmRuleDto>>();
        var result = new PaginatedListDto<AlarmRuleDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<AlarmRule, bool>>> CreateFilteredPredicate(GetAlarmRuleInputDto inputDto)
    {
        Expression<Func<AlarmRule, bool>> condition = x => true;

        return await Task.FromResult(condition); ;
    }
}
