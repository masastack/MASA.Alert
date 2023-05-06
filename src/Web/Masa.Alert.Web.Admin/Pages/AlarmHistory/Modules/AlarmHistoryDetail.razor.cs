// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Humanizer;

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class AlarmHistoryDetail : AdminCompontentBase
{
    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    [Parameter]
    public AlarmHistoryViewModel AlarmHistory { get; set; } = new();

    [Parameter]
    public bool HandleStatusShow { get; set; }

    private bool _handleAlarmModalVisible;

    private AlarmHistoryHandleDetail? _handleDetail;

    private bool _isThirdParty;

    private AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    private void HandleOnClick(AlarmRuleRecordListViewModel record)
    {
        AlarmHistory.RuleResultItems = record.RuleResultItems;
        AlarmHistory.LastAlarmTime = record.CreationTime.ToLocalTime();
    }

    private async Task HandleDetailEditAsync()
    {
        _handleAlarmModalVisible = true;
    }

    private async Task HandleDetailSaveAsync()
    {
        Check.NotNull(_handleDetail.Form, "form not found");

        if (!_handleDetail.Form.Validate())
        {
            return;
        }

        Loading = true;

        var inputDto = AlarmHistory.Handle.Adapt<AlarmHandleDto>();

        if (!_isThirdParty)
        {
            inputDto.WebHookId = default;
        }

        try
        {
            await AlarmHistoryService.HandleAsync(AlarmHistory.Id, inputDto);
        }
        catch (Exception ex)
        {
            Loading = false;
            await PopupService.EnqueueSnackbarAsync(ex.Message, AlertTypes.Error);
            return;
        }

        Loading = false;
        _handleAlarmModalVisible = false;

        await SuccessMessageAsync(T("OperationSuccessfulMessage"));

        await ResetFormAsync();
    }

    private async Task ResetFormAsync()
    {
        _isThirdParty = false;
        await GetFormDataAsync();
    }

    private async Task GetFormDataAsync()
    {
        var alarmHistory = await AlarmHistoryService.GetAsync(AlarmHistory.Id);
        AlarmHistory = alarmHistory.Adapt<AlarmHistoryViewModel>();
    }
}
