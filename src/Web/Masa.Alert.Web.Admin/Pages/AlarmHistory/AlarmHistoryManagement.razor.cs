// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory;

public partial class AlarmHistoryManagement : AdminCompontentBase
{
    private GetAlarmHistoryInputDto _queryParam = new(10);
    private bool advanced = false;
    private bool isAnimate;
    private List<DataTableHeader<AlarmHistoryListViewModel>> _headers = new();
    private PaginatedListDto<AlarmHistoryListViewModel> _entities = new();
    private AlarmHistoryDetailModal? _detailModal;
    private HandleAlarmModal? _handleAlarmModal;

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    protected override string? PageName { get; set; } = "AlarmHistory";

    protected override void OnInitialized()
    {
        _headers = new()
        {
            new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false},
            new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false},
            new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime)},
            new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount)},
            new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime)},
            new() { Text = T(nameof(AlarmHistoryListViewModel.Status)), Value = nameof(AlarmHistoryListViewModel.Status), Sortable = false},
            new() { Text = T("Action"), Value = "Action"},
        };
    }

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
        var dtos = (await AlarmHistoryService.GetListAsync(_queryParam));
        _entities = dtos?.Adapt<PaginatedListDto<AlarmHistoryListViewModel>>() ?? new();
        Loading = false;
        StateHasChanged();
    }

    private async Task RefreshAsync()
    {
        _queryParam.Page = 1;
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
}
