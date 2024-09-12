// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.AlarmRules.Queries;

public class AlarmRuleQueryHandler
{
    private readonly IAlertQueryContext _context;
    private readonly IAuthClient _authClient;
    private readonly IPmClient _pmClient;
    private IMultiEnvironmentContext _multiEnvironment;
    private readonly II18n<DefaultResource> _i18n;

    public AlarmRuleQueryHandler(IAlertQueryContext context, IAuthClient authClient, II18n<DefaultResource> i18n, IPmClient pmClient, IMultiEnvironmentContext multiEnvironment)
    {
        _context = context;
        _authClient = authClient;
        _pmClient = pmClient;
        _multiEnvironment = multiEnvironment;
        _i18n = i18n;
    }

    [EventHandler]
    public async Task GetAsync(GetAlarmRuleQuery query)
    {
        var entity = await _context.AlarmRuleQueries.Include(x => x.Items).Include(x => x.LogMonitorItems).Include(x => x.MetricMonitorItems).FirstOrDefaultAsync(x => x.Id == query.AlarmRuleId);

        MasaArgumentException.ThrowIfNull(entity, _i18n.T("AlarmRule"));

        query.Result = entity.Adapt<AlarmRuleDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetAlarmRuleListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var resultList = await _context.AlarmRuleQueries.Include(x => x.Items).Include(x => x.MetricMonitorItems).GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(AlarmRuleQueryModel.ModificationTime)] = true
            }
        });

        var dtos = resultList.Result.Adapt<List<AlarmRuleDto>>();
        await FillAlarmRuleDtos(dtos);
        var result = new PaginatedListDto<AlarmRuleDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<AlarmRuleQueryModel, bool>>> CreateFilteredPredicate(GetAlarmRuleInputDto options)
    {
        Expression<Func<AlarmRuleQueryModel, bool>> condition = x => true;
        condition = condition.And(!string.IsNullOrEmpty(options.Filter), x => x.DisplayName.Contains(options.Filter));
        condition = condition.And(options.Type != default, x => x.Type == options.Type);
        condition = condition.And(x => x.Show == options.Show);
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
        await AppendProjectAppFilter(options, condition);
        condition = condition.And(!string.IsNullOrEmpty(options.MetricId), x => x.MetricMonitorItems.Any(x => x.Aggregation.Name == options.MetricId));
        return condition;
    }

    private async Task AppendProjectAppFilter(GetAlarmRuleInputDto options, Expression<Func<AlarmRuleQueryModel, bool>> condition)
    {
        var projects = await _pmClient.ProjectService.GetListByTeamIdsAsync(new List<Guid>() { options.TeamId }, _multiEnvironment.CurrentEnvironment);
        if (projects == null || !projects.Any())
        {
            condition = condition.And(x => false);
            return;
        }

        if (!string.IsNullOrEmpty(options.ProjectIdentity))
        {
            if (!projects.Exists(project => project.Identity == options.ProjectIdentity))
            {
                condition = condition.And(y => false);
                return;
            }

            if (!string.IsNullOrEmpty(options.AppIdentity))
            {
                var project = projects.Find(p => p.Identity == options.ProjectIdentity)!;
                var apps = await _pmClient.AppService.GetListByProjectIdsAsync(new List<int>() { project.Id });
                if (apps == null || !apps.Exists(app => app.Identity == options.AppIdentity))
                {
                    condition = condition.And(y => false);
                    return;
                }
            }
        }
    }

    private async Task FillAlarmRuleDtos(List<AlarmRuleDto> dtos)
    {
        var modifierUserIds = dtos.Where(x => x.Modifier != Guid.Empty).Select(x => x.Modifier).Distinct().ToArray();
        var userInfos = await _authClient.UserService.GetListByIdsAsync(modifierUserIds);
        foreach (var item in dtos)
        {
            var modifier = userInfos.Find(x => x.Id == item.Modifier);
            item.ModifierName = modifier?.RealDisplayName ?? string.Empty;
        }
    }
}
