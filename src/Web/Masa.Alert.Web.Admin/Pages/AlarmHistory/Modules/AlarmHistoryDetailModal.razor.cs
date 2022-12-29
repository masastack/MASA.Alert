// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class AlarmHistoryDetailModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private bool _visible;
    private Guid _entityId;
    private AlarmHistoryViewModel _model = new();

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    public async Task OpenModalAsync(AlarmHistoryListViewModel? listModel = null)
    {
        _entityId = listModel?.Id ?? default;
        _model = listModel?.Adapt<AlarmHistoryViewModel>() ?? new();

        if (_entityId != default)
        {
            await GetFormDataAsync();
        }

        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });
    }

    private async Task GetFormDataAsync()
    {
        var dto = await AlarmHistoryService.GetAsync(_entityId) ?? new();
        _model = dto.Adapt<AlarmHistoryViewModel>();
    }

    private void HandleCancel()
    {
        _visible = false;
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private async Task HandleDel()
    {
        await ConfirmAsync(T("DeletionConfirmationMessage", $"{T("AlarmHistory")}"), DeleteAsync, AlertTypes.Error);
    }

    private async Task DeleteAsync()
    {
        Loading = true;
        await AlarmHistoryService.DeleteAsync(_entityId);
        Loading = false;
        await SuccessMessageAsync(T("DeletedSuccessfullyMessage"));
        _visible = false;
        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }
}
