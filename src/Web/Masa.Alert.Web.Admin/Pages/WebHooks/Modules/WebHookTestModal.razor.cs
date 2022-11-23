// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.WebHooks.Modules;

public partial class WebHookTestModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private WebHookTestViewModal _model = new();
    private bool _visible;

    WebHookService WebHookService => AlertCaller.WebHookService;

    public async Task OpenModalAsync(Guid webHookId)
    {
        _model.WebHookId = webHookId;
        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });
    }

    private void HandleCancel()
    {
        _visible = false;
        ResetForm();
    }

    private async Task HandleOkAsync()
    {
        Loading = true;
        await WebHookService.TestAsync(_model.WebHookId, new WebHookTestDto { Handler = _model.UserId });
        Loading = false;
        await SuccessMessageAsync(T("OperationSuccessfulMessage"));
        _visible = false;

        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
        ResetForm();
    }

    private void ResetForm()
    {
        _model = new();
    }
}
