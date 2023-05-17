// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.WebHooks.Modules;

public partial class WebHookUpsertModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private MForm? _form;
    private WebHookUpsertViewModel _model = new();
    private bool _visible;
    private Guid _entityId;
    private WebHookTestModal? _testModal;

    protected override string? PageName { get; set; } = "WebHookBlock";

    WebHookService WebHookService => AlertCaller.WebHookService;

    public async Task OpenModalAsync(WebHookListViewModel? listModel = null)
    {
        _entityId = listModel?.Id ?? default;
        _model = listModel?.Adapt<WebHookUpsertViewModel>() ?? new();

        if (_entityId != default)
        {
            await GetFormDataAsync();
        }

        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });

        _form?.ResetValidation();
    }

    private async Task GetFormDataAsync()
    {
        var dto = await WebHookService.GetAsync(_entityId) ?? new();
        _model = dto.Adapt<WebHookUpsertViewModel>();
    }

    private void HandleCancel()
    {
        _visible = false;
        ResetForm();
    }

    private void ResetForm()
    {
        _model = new();
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private void HandleRefresh()
    {
        _model.SecretKey = Guid.NewGuid().ToString();
    }

    private async Task HandleOk()
    {
        Check.NotNull(_form, "form not found");

        if (!_form.Validate())
        {
            return;
        }

        Loading = true;

        var inputDto = _model.Adapt<WebHookUpsertDto>();

        if (_entityId == default)
        {
            await WebHookService.CreateAsync(inputDto);
        }
        else
        {
            await WebHookService.UpdateAsync(_entityId, inputDto);
        }

        Loading = false;
        _visible = false;

        ResetForm();

        await UpsertSuccessfulMessage(_entityId, T("WebHook"));

        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }

    private async Task HandleDel()
    {
        await ConfirmAsync(T("DeletionConfirmationMessage", $"{T("WebHook")}\"{_model.DisplayName}\""), DeleteAsync, AlertTypes.Error);
    }

    private async Task DeleteAsync()
    {
        Loading = true;
        await WebHookService.DeleteAsync(_entityId);
        Loading = false;
        await SuccessMessageAsync(T("DeletedSuccessfullyMessage"));
        _visible = false;
        ResetForm();
        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }
}
