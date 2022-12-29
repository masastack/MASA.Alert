// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.WebHooks;

public partial class WebHookManagement : AdminCompontentBase
{
    private GetWebHookInputDto _queryParam = new(20);
    private PaginatedListDto<WebHookListViewModel> _entities = new();
    private WebHookUpsertModal? _upsertModal;
    private WebHookTestModal? _testModal;
    protected override string? PageName { get; set; } = "WebHookBlock";

    WebHookService WebHookService => AlertCaller.WebHookService;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task LoadData()
    {
        Loading = true;
        var dtos = (await WebHookService.GetListAsync(_queryParam));
        _entities = dtos?.Adapt<PaginatedListDto<WebHookListViewModel>>() ?? new();
        Loading = false;
        StateHasChanged();
    }

    private async Task HandleOk()
    {
        await LoadData();
    }

    private async Task RefreshAsync()
    {
        _queryParam.Page = 1;
        await LoadData();
    }

    private async Task HandlePageChanged(int page)
    {
        _queryParam.Page = page;
        await LoadData();
    }

    private async Task HandlePageSizeChanged(int pageSize)
    {
        _queryParam.PageSize = pageSize;
        await LoadData();
    }

    private async Task HandleClearAsync()
    {
        _queryParam = new(20);
        await LoadData();
    }
}
