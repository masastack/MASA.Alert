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
    private List<DataTableHeader<AlarmHistoryListViewModel>> _headers = new();
    private PaginatedListDto<AlarmHistoryListViewModel> _entities = new();
    private AlarmHistoryDetailModal? _detailModal;
    private HandleAlarmModal? _handleAlarmModal;
    private List<AlarmHistorySearchTimeTypes> _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().Where(x => x != AlarmHistorySearchTimeTypes.ProcessingCompletedTime).ToList();
    private List<AlarmHistoryHandleStatuses> _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;
    AlarmRuleService AlarmRuleService => AlertCaller.AlarmRuleService;

    protected override string? PageName { get; set; } = "AlarmHistoryBlock";

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

    private void HandleHeaders()
    {
        if (_queryParam.SearchType == AlarmHistorySearchTypes.Alarming)
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.HandleStatus)), Value = nameof(AlarmHistoryListViewModel.HandleStatus), Sortable = false},
                new() { Text = T("Action"), Value = "Action"},
            };
        }
        else if (_queryParam.SearchType == AlarmHistorySearchTypes.Processed)
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false},
                new() { Text = T("HandleCompletionTime"), Value = "HandleCompletionTime"},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime)},
                new() { Text = T("Action"), Value = "Action"},
            };
        }
        else
        {
            _headers = new()
            {
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false},
                new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false},
                new() { Text = T(nameof(AlarmHistoryListViewModel.FirstAlarmTime)), Value = nameof(AlarmHistoryListViewModel.FirstAlarmTime)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.AlarmCount)), Value = nameof(AlarmHistoryListViewModel.AlarmCount)},
                new() { Text = T(nameof(AlarmHistoryListViewModel.LastAlarmTime)), Value = nameof(AlarmHistoryListViewModel.LastAlarmTime)},
                new() { Text = T("Action"), Value = "Action"},
            };
        }
    }

    private async Task LoadData()
    {
        HandleHeaders();
        Loading = true;

        var queryParam = _queryParam.Adapt<GetAlarmHistoryInputDto>() ?? new();
        queryParam.StartTime = queryParam.StartTime?.Add(TimezoneOffset);
        queryParam.EndTime = queryParam.EndTime?.Add(TimezoneOffset);

        var dtos = (await AlarmHistoryService.GetListAsync(queryParam));
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
                _queryParam.Sorting = $"{nameof(AlarmHistoryQueryModel.AlertSeverity)} asc,{nameof(AlarmHistoryQueryModel.FirstAlarmTime)} asc";
                break;
            case AlarmHistorySearchTypes.Processed:
                _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().ToList();
                _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();
                _queryParam.TimeType = AlarmHistorySearchTimeTypes.ProcessingCompletedTime;
                _queryParam.Sorting = $"{nameof(AlarmHistoryQueryModel.RecoveryTime)} desc";
                break;
            case AlarmHistorySearchTypes.NoNotice:
                _timeTypeItems = Enum.GetValues<AlarmHistorySearchTimeTypes>().Where(x => x != AlarmHistorySearchTimeTypes.ProcessingCompletedTime).ToList();
                _handleStatusItems = Enum.GetValues<AlarmHistoryHandleStatuses>().ToList();
                _queryParam.TimeType = default;
                _queryParam.Sorting = $"{nameof(AlarmHistoryQueryModel.LastAlarmTime)} desc";
                break;
            default:
                break;
        }

        await RefreshAsync();
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
