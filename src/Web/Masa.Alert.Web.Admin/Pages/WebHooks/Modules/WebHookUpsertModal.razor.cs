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

    private WebHookTestModal? _testModal;

    protected override string? PageName { get; set; } = "WebHook";

    public async Task OpenModalAsync(WebHookListViewModel? listModel = null)
    {
        _model = listModel?.Adapt<WebHookUpsertViewModel>() ?? new();
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

    private void ResetForm()
    {
        _model = new();
        _form?.ResetValidation();
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private void HandleRefresh()
    {
        _model.SecretKey = Guid.NewGuid().ToString();
    }
}
