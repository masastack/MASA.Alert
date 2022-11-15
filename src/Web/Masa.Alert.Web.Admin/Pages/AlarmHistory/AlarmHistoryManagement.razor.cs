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

    protected override string? PageName { get; set; } = "AlarmHistory";

    protected override void OnInitialized()
    {
        _headers = new()
        {
            new() { Text = T(nameof(AlarmHistoryListViewModel.AlertSeverity)), Value = nameof(AlarmHistoryListViewModel.AlertSeverity),Sortable=false},
            new() { Text = T(nameof(AlarmHistoryListViewModel.DisplayName)), Value = nameof(AlarmHistoryListViewModel.DisplayName),Sortable=false},
            new() { Text = T(nameof(AlarmHistoryListViewModel.TriggerRules)), Value = nameof(AlarmHistoryListViewModel.TriggerRules), Sortable = false},
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
        FillData();
        await Task.CompletedTask;
        Loading = false;
        StateHasChanged();
    }

    private void FillData()
    {
        for (int i = 0; i < 20; i++)
        {
            _entities.Result.Add(new AlarmHistoryListViewModel
            {
                DisplayName = "库存告警通知库存告警通知库存告警通知",
                AlertSeverity = (AlertSeverity)(i % 5) + 1,
                TriggerRules = "商品库存数量小于20 ",
                FirstAlarmTime = DateTime.Now,
                AlarmCount = 1,
                LastAlarmTime = DateTime.Now,
                Status = (AlarmHistoryStatuses)(i % 2) + 1,
                AlarmRule = new AlarmRuleViewModel
                {
                    DisplayName = "库存告警通知库存告警通知库存...",
                    ProjectIdentity = "Masa.Auth",
                    AppIdentity = "masa-auth-web-admin",
                    IsEnabled = i % 6 != 0,
                    CheckFrequency = new CheckFrequencyViewModel
                    {
                        Type = AlarmCheckFrequencyTypes.FixedInterval,
                        FixedInterval = new TimeIntervalViewModel
                        {
                            IntervalTime = 60,
                            IntervalTimeType = TimeTypes.Minute,
                        }
                    },
                    LogMonitorItems = new List<LogMonitorItemViewModel>
                    {
                        new LogMonitorItemViewModel
                        {
                            Field="_goodsid_",
                            IsOffset=true,
                            OffsetPeriod=15,
                            Alias="_goodsidcount_"
                        }
                    }
                },
            });
        }
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
