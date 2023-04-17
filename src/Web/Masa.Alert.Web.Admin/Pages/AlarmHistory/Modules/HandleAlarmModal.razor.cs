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
    private AlarmHandleViewModel _handle = new();
    private string _tab = "";
    private bool _isThirdParty;
    private MForm? _form;

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    WebHookService WebHookService => AlertCaller.WebHookService;

    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

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
        _handle = _model.Handle;
    }

    private void HandleCancel()
    {
        _visible = false;
        ResetForm();
    }

    private void ResetForm()
    {
        _handle = new();
        _isThirdParty = false;
        _tab = T("AlarmDetails");
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private async void HandleAlarm()
    {
        Check.NotNull(_form, "form not found");

        if (!_form.Validate())
        {
            return;
        }

        Loading = true;
        var inputDto = _model.Handle.Adapt<AlarmHandleDto>();

        try
        {
            await AlarmHistoryService.HandleAsync(_entityId, inputDto);
        }
        catch (Exception ex)
        {
            Loading = false;
            await PopupService.EnqueueSnackbarAsync(ex.Message, AlertTypes.Error);
            return;
        }

        Loading = false;
        _visible = false;

        ResetForm();

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
            _handle.WebHookId = default;
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
