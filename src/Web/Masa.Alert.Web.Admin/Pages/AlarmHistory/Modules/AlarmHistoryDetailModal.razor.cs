// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class AlarmHistoryDetailModal : AdminCompontentBase
{
    private bool _visible;
    private AlarmHistoryViewModel _model = new();

    protected override string? PageName { get; set; } = "AlarmHistory";

    public async Task OpenModalAsync(AlarmHistoryListViewModel? listModel = null)
    {
        _model = listModel?.Adapt<AlarmHistoryViewModel>() ?? new();
        FillData();
        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });
    }

    private void FillData()
    {
        _model.AlarmRuleItems.Add(new AlarmRuleItemViewModel
        {
            AlertSeverity = AlertSeverity.High,
            Expression = "表达式XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
            MessageTemplateName = "商品库存小于0的通知",
            IsRecoveryNotification = true,
            IsNotification = true,
        });
        _model.AlarmRuleItems.Add(new AlarmRuleItemViewModel
        {
            AlertSeverity = AlertSeverity.Low,
            Expression = "表达式XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
            MessageTemplateName = "商品库存小于0的通知",
            IsRecoveryNotification = true,
            IsNotification = true,
        });
    }

    private void HandleCancel()
    {
        _visible = false;
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }
}
