// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class HandleAlarmModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private bool _visible;
    private Guid _entityId;
    private AlarmHistoryViewModel _model = new();
    private string _tab = "";
    private List<WebHookListViewModel> _webHookItems = new();
    private bool _isThirdParty;

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    WebHookService WebHookService => AlertCaller.WebHookService;

    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _tab = T("AlarmDetails");
            await GetWebHooks();
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

    private async Task GetWebHooks()
    {
        var dtos = await WebHookService.GetListAsync(new GetWebHookInputDto(10));
        _webHookItems = dtos?.Adapt<PaginatedListDto<WebHookListViewModel>>().Result ?? new();
    }

    private async Task GetFormDataAsync()
    {
        var dto = await AlarmHistoryService.GetAsync(_entityId) ?? new();
        _model = dto.Adapt<AlarmHistoryViewModel>();
    }

    private void HandleCancel()
    {
        _visible = false;
        ResetForm();
    }

    private void ResetForm()
    {
        _model = new();
        _tab = T("AlarmDetails");
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
        _visible = false;
        await SuccessMessageAsync(T("OperationSuccessfulMessage"));

        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }

    private void SelectAlarmHandle(bool isThirdParty)
    {
        _isThirdParty = isThirdParty;

        if (!isThirdParty)
        {
            _model.Handle.WebHookId = default;
        }
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
