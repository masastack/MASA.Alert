// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmRules;

public partial class AlarmRuleManagement : AdminCompontentBase
{
    private GetAlarmRuleInputDto _queryParam = new(20);
    private PaginatedListDto<AlarmRuleListViewModel> _entities = new();
    private bool advanced = false;
    private bool isAnimate;

    protected override async Task OnInitializedAsync()
    {
        PageName = "AlarmRule";
        await base.OnInitializedAsync();
    }

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
        Loading = false;
        StateHasChanged();
    }

    private void FillData()
    {
        for (int i = 0; i < 20; i++)
        {
            _entities.Result.Add(new AlarmRuleListViewModel
            {
                DisplayName = "库存告警通知库存告警通知库存...",
                ProjectId = "Masa.Auth",
                AppId = "masa-auth-web-admin",
                ModifierName = "李西瓜",
                ModificationTime = DateTime.Now
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

    private void ToggleAdvanced()
    {
        advanced = !advanced;
        isAnimate = true;
    }

    private async Task HandleDelAsync(Guid _entityId)
    {
        await ConfirmAsync(T("DeletionConfirmationMessage"), async () => { await DeleteAsync(_entityId); });
    }

    private async Task DeleteAsync(Guid _entityId)
    {
        Loading = true;
        Loading = false;
        await SuccessMessageAsync(T("MessageTaskDeleteMessage"));
        await LoadData();
    }
}
