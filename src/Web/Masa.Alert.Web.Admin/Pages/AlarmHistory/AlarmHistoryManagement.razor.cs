// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory;

public partial class AlarmHistoryManagement : AdminCompontentBase
{
    [Parameter]
    public string AlarmRuleId { get; set; } = string.Empty;

    private GetAlarmHistoryInputDto _queryParam = new(10);
    private bool advanced = false;
    private bool isAnimate;
    private PaginatedListDto<AlarmHistoryListViewModel> _entities = new();
    private AlarmHistoryDetailModal? _detailModal;
    private HandleAlarmModal? _handleAlarmModal;
    private List<AlarmHistorySearchTimeTypes> _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().Where(x => x != AlarmHistorySearchTimeTypes.ProcessingCompletedTime).ToList();
    private List<AlarmHistoryHandleStatuses> _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;
    AlarmRuleService AlarmRuleService => AlertCaller.AlarmRuleService;

    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

    private SDataTable<AlarmHistoryListViewModel> _dataTableRef;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async void OnParametersSet()
    {
        _queryParam.AlarmRuleId = string.IsNullOrEmpty(AlarmRuleId) ? null : Guid.Parse(AlarmRuleId);

        if (_queryParam.AlarmRuleId.HasValue)
        {
            var alarmRule = await AlarmRuleService.GetAsync(_queryParam.AlarmRuleId.Value);
            _queryParam.Filter = alarmRule?.DisplayName ?? string.Empty;
        }
        else
        {
            _queryParam.Filter = string.Empty;
        }
    }

    public List<DataTableHeader<AlarmHistoryListViewModel>> GetHeaders()
    {
        List<DataTableHeader< AlarmHistoryListViewModel >> _headers = new();

        if (_queryParam.SearchType == AlarmHistorySearchTypes.Alarming)
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false, Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false, Width = "19.6875rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.HandleStatus)), Value = nameof(AlarmHistoryListViewModel.HandleStatus), Sortable = false, Width = "13.125rem"},
                new() { Text = T("Action"), Value = "Action",Sortable=false,Width=105},
            };
        }
        else if (_queryParam.SearchType == AlarmHistorySearchTypes.Processed)
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false, Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false, Width = "19.6875rem"},
                new() { Text = T("HandleCompletionTime"), Value = nameof(AlarmHistoryQueryModel.RecoveryTime), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime), Width = "6.5625rem"},
                new() { Text = T("Action"), Value = "Action",Sortable=false,Width=105},
            };
        }
        else
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false, Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false, Width = "19.6875rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount), Width = "6.5625rem"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime), Width = "6.5625rem"},
                new() { Text = T("Action"), Value = "Action",Sortable=false,Width=105},
            };
        }
        return _headers;
    }

    private async Task LoadData()
    {
        Loading = true;

        var queryParam = _queryParam.Adapt<GetAlarmHistoryInputDto>() ?? new();
        queryParam.StartTime = queryParam.StartTime?.Add(JsInitVariables.TimezoneOffset);
        queryParam.EndTime = queryParam.EndTime?.Add(JsInitVariables.TimezoneOffset);

        var dtos = (await AlarmHistoryService.GetListAsync(queryParam));
        _entities = dtos?.Adapt<PaginatedListDto<AlarmHistoryListViewModel>>() ?? new();
        Loading = false;
        StateHasChanged();
    }

    private Task DateRangChangedAsync((DateTimeOffset?, DateTimeOffset?) args)
    {
        (_queryParam.StartTime, _queryParam.EndTime) = (args.Item1?.UtcDateTime, args.Item2?.UtcDateTime);
        return RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        _queryParam.Page = 1;
        await LoadData();
    }

    private async Task HandleClearAsync()
    {
        _queryParam = new(10);
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

    private async Task HandleOk()
    {
        await LoadData();
    }

    private async Task HandleSearchTypeChange()
    {
        _queryParam = new() { Filter = _queryParam.Filter, SearchType = _queryParam.SearchType, AlarmRuleId = _queryParam.AlarmRuleId, Page = 1 };
        switch (_queryParam.SearchType)
        {
            case AlarmHistorySearchTypes.Alarming:
                _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().Where(x => x != AlarmHistorySearchTimeTypes.ProcessingCompletedTime).ToList();
                _handleStatusItems = new List<AlarmHistoryHandleStatuses> { AlarmHistoryHandleStatuses.Pending, AlarmHistoryHandleStatuses.InProcess };
                _queryParam.TimeType = default;

                HandleSort(nameof(AlarmHistoryQueryModel.AlertSeverity));
                break;
            case AlarmHistorySearchTypes.Processed:
                _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().ToList();
                _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();
                _queryParam.TimeType = AlarmHistorySearchTimeTypes.ProcessingCompletedTime;

                HandleSort(nameof(AlarmHistoryQueryModel.RecoveryTime), true);
                break;
            case AlarmHistorySearchTypes.NoNotice:
                _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().Where(x => x != AlarmHistorySearchTimeTypes.ProcessingCompletedTime).ToList();
                _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();
                _queryParam.TimeType = default;

                HandleSort(nameof(AlarmHistoryQueryModel.LastAlarmTime), true);
                break;
            default:
                break;
        }

        await RefreshAsync();
    }

    private void HandleSort(string sortField, bool isDesc = false)
    {
        _queryParam.Sorting = $"{sortField} {(isDesc ? "desc" : "")}";
        _dataTableRef.UpdateOptions(options =>
        {
            var sortBys = new List<string> { sortField };
            _dataTableRef.SortArray(sortBys);
        });
    }



    private async Task HandleFilterKeyDown()
    {
        if (string.IsNullOrEmpty(_queryParam.Filter))
        {
            _queryParam.AlarmRuleId = null;
        }
        await RefreshAsync();
    }

    private async Task HandleOnOptionsUpdate(DataOptions options)
    {
        var sortBy = options.SortBy;
        var sorting = new StringBuilder();

        for (int i = 0; i < sortBy.Count; i++)
        {
            if (i > 0)
            {
                sorting.AppendLine(",");
            }
            var sortDesc = options.SortDesc[i] ? "desc" : "asc";
            sorting.AppendLine($"{sortBy[i]} {sortDesc}");
        }

        _queryParam.Sorting = sorting.ToString();

        await RefreshAsync();
    }
}
