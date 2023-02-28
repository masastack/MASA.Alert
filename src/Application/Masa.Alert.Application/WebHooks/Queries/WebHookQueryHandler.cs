// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Application.WebHooks.Queries;

public class WebHookQueryHandler
{
    private readonly IAlertQueryContext _context;
    private readonly IAuthClient _authClient;

    public WebHookQueryHandler(IAlertQueryContext context, IAuthClient authClient)
    {
        _context = context;
        _authClient = authClient;
    }

    [EventHandler]
    public async Task GetAsync(GetWebHookQuery query)
    {
        var entity = await _context.WebHookQueries.FirstOrDefaultAsync(x => x.Id == query.WebHookId);

        Check.NotNull(entity, "WebHook not found");

        query.Result = entity.Adapt<WebHookDto>();
    }

    [EventHandler]
    public async Task GetListAsync(GetWebHookListQuery query)
    {
        var options = query.Input;
        var condition = await CreateFilteredPredicate(options);
        var resultList = await _context.WebHookQueries.GetPaginatedListAsync(condition, new()
        {
            Page = options.Page,
            PageSize = options.PageSize,
            Sorting = new Dictionary<string, bool>
            {
                [nameof(WebHookQueryModel.ModificationTime)] = true
            }
        });

        var dtos = resultList.Result.Adapt<List<WebHookDto>>();
        await FillWebHookDtos(dtos);
        var result = new PaginatedListDto<WebHookDto>(resultList.Total, resultList.TotalPages, dtos);
        query.Result = result;
    }

    private async Task<Expression<Func<WebHookQueryModel, bool>>> CreateFilteredPredicate(GetWebHookInputDto options)
    {
        Expression<Func<WebHookQueryModel, bool>> condition = x => true;
        condition = condition.And(!string.IsNullOrEmpty(options.Filter), x => x.DisplayName.Contains(options.Filter));
        return await Task.FromResult(condition); ;
    }

    private async Task FillWebHookDtos(List<WebHookDto> dtos)
    {
        var modifierUserIds = dtos.Where(x => x.Modifier != default).Select(x => x.Modifier).Distinct().ToArray();
        var userInfos = await _authClient.UserService.GetListByIdsAsync(modifierUserIds);
        foreach (var item in dtos)
        {
            item.ModifierName = userInfos.FirstOrDefault(x => x.Id == item.Modifier)?.StaffDislpayName ?? string.Empty;
        }
    }
}
