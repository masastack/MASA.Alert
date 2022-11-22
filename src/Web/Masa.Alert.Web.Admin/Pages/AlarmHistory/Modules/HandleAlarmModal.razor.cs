// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Humanizer;

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class HandleAlarmModal : AdminCompontentBase
{
    private bool _visible;
    private Guid _entityId;
    private AlarmHistoryViewModel _model = new();
    private string _tab = "";
    private List<string> _items = new();
    private bool _isThirdParty;

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    protected override string? PageName { get; set; } = "AlarmHistory";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _tab = T("AlarmDetails");
        }
        await base.OnAfterRenderAsync(firstRender);
    }

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

    private async void HandleAlarm()
    {
        Loading = true;
        var inputDto = _model.Handle.Adapt<AlarmHandleDto>();
        await AlarmHistoryService.HandleAsync(_entityId, inputDto);
        Loading = false;
        await SuccessMessageAsync(T("OperationSuccessfulMessage"));
    }

    private void SelectAlarmHandle(bool isThirdParty)
    {
        _isThirdParty = isThirdParty;

        if (!isThirdParty)
        {
            _model.Handle.WebHookId = default;
        }
    }
}
