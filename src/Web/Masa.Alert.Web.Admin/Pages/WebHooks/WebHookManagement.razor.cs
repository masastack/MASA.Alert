// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.WebHooks;

public partial class WebHookManagement : AdminCompontentBase
{
    private GetWebHookInputDto _queryParam = new(20);
    private PaginatedListDto<WebHookListViewModel> _entities = new();
    private WebHookUpsertModal? _upsertModal;
    private WebHookTestModal? _testModal;
    protected override string? PageName { get; set; } = "WebHook";

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
        FillData();
        await Task.CompletedTask;
        Loading = false;
        StateHasChanged();
    }

    private void FillData()
    {
        for (int i = 0; i < 20; i++)
        {
            _entities.Result.Add(new WebHookListViewModel
            {
                DisplayName = "名称XXXX",
                Url = "www.masastack.com",
                Description = "描述说明描述说明描述说明",
                ModifierName = "李西瓜",
                ModificationTime = DateTime.Now,
                SecretKey = "6F9619FF-8B86-D011-B42D-00C04FC964FF"
            });
        }
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

    private async Task HandleDelAsync(Guid _entityId)
    {
        await ConfirmAsync(T("DeletionConfirmationMessage"), async () => { await DeleteAsync(_entityId); });
    }

    private async Task DeleteAsync(Guid _entityId)
    {
        Loading = true;
        Loading = false;
        await SuccessMessageAsync(T("DeletedSuccessfullyMessage"));
        await LoadData();
    }
}
