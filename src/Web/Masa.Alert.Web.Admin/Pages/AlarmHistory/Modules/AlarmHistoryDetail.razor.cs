// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class AlarmHistoryDetail : AdminCompontentBase
{
    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    [Parameter]
    public AlarmHistoryViewModel AlarmHistory { get; set; } = new();

    [Parameter]
    public bool HandleStatusShow { get; set; }

    private bool _handleAlarmModalVisible;

    private HandleAlarm? _handleDetail;

    private AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    private void HandleOnClick(AlarmRuleRecordListViewModel record)
    {
        AlarmHistory.RuleResultItems = record.RuleResultItems;
        AlarmHistory.LastAlarmTime = record.CreationTime.ToLocalTime();
    }

    private void HandleChange()
    {
        _handleAlarmModalVisible = true;
    }

    private async Task HandleDetailSaveAsync()
    {
        Check.NotNull(_handleDetail, "form not found");

        var handleResult = await _handleDetail.Submit();

        if (!handleResult)
            return;

        _handleAlarmModalVisible = false;

        await ResetFormAsync();
    }

    private async Task ResetFormAsync()
    {
        await GetFormDataAsync();
    }

    private async Task GetFormDataAsync()
    {
        var alarmHistory = await AlarmHistoryService.GetAsync(AlarmHistory.Id);
        AlarmHistory = alarmHistory.Adapt<AlarmHistoryViewModel>();
    }
}
