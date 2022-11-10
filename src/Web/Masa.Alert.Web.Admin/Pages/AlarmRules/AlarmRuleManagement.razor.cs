// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmRules;

public partial class AlarmRuleManagement : AdminCompontentBase
{
    private GetAlarmRuleInputDto _queryParam = new(20);
    private PaginatedListDto<AlarmRuleListViewModel> _entities = new();
    private bool advanced = false;
    private bool isAnimate;
    private LogAlarmRuleUpsertModal? _logUpsertModal;
    private MetricAlarmRuleUpsertModal? _metricUpsertModal;

    AlarmRuleService AlarmRuleService => AlertCaller.AlarmRuleService;

    protected override string? PageName { get; set; } = "AlarmRule";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task LoadData()
    {
        Loading = true;
        var dtos = (await AlarmRuleService.GetListAsync(_queryParam));
        _entities = dtos?.Adapt<PaginatedListDto<AlarmRuleListViewModel>>() ?? new();
        Loading = false;
        StateHasChanged();
    }

    private async Task HandleOk()
    {
        await LoadData();
    }

    private async Task RefreshAsync()
    {
        _queryParam.Page = 1;
        await LoadData();
    }

    private async Task HandlePageChanged(int page)
    {
        _queryParam.Page = page;
        await LoadData();
    }

    private async Task HandlePageSizeChanged(int pageSize)
    {
        _queryParam.PageSize = pageSize;
        await LoadData();
    }

    private async Task HandleClearAsync()
    {
        _queryParam = new(20);
        await LoadData();
    }

    private void ToggleAdvanced()
    {
        advanced = !advanced;
        isAnimate = true;
    }

    private async Task HandleDelAsync(Guid _entityId)
    {
        await ConfirmAsync(T("DeletionConfirmationMessage"), async () => { await DeleteAsync(_entityId); });
    }

    private async Task DeleteAsync(Guid _entityId)
    {
        Loading = true;
        Loading = false;
        await SuccessMessageAsync(T("DeletedSuccessfullyMessage"));
        await LoadData();
    }

    private async Task HandleAdd()
    {
        if (_queryParam.AlarmRuleType== AlarmRuleTypes.Log)
        {
            await _logUpsertModal?.OpenModalAsync()!;
        }

        if (_queryParam.AlarmRuleType == AlarmRuleTypes.Metric)
        {
            await _metricUpsertModal?.OpenModalAsync()!;
        }
    }

    private async Task HandleEdit(AlarmRuleListViewModel item)
    {
        if (_queryParam.AlarmRuleType == AlarmRuleTypes.Log)
        {
            await _logUpsertModal?.OpenModalAsync(item)!;
        }

        if (_queryParam.AlarmRuleType == AlarmRuleTypes.Metric)
        {
            await _metricUpsertModal?.OpenModalAsync(item)!;
        }
    }
}
