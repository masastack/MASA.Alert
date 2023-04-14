// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Contrib.SearchEngine.AutoComplete.ElasticSearch;

namespace Masa.Alert.Web.Admin.Pages.AlarmRules;

public partial class AlarmRuleManagement : AdminCompontentBase
{
    [Inject]
    public IPmClient PmClient { get; set; } = default!;

    [Inject]
    public ITscClient TscClient { get; set; } = default!;

    private GetAlarmRuleInputDto _queryParam = new(20);
    private PaginatedListDto<AlarmRuleListViewModel> _entities = new();
    private bool advanced = false;
    private bool isAnimate;
    private LogAlarmRuleUpsertModal? _logUpsertModal;
    private MetricAlarmRuleUpsertModal? _metricUpsertModal;
    private List<ProjectModel> _projects = new();
    private List<SelectItem<string>> _projectItems = new();
    private List<SelectItem<string>> _appsItems = new();
    private List<SelectItem<string>> _metricItems = new();

    AlarmRuleService AlarmRuleService => AlertCaller.AlarmRuleService;

    protected override string? PageName { get; set; } = "AlarmRuleBlock";

    private bool _showEmptyPlaceholder = false;

    private readonly AsyncTaskQueue _asyncTaskQueue;

    public AlarmRuleManagement()
    {
        _asyncTaskQueue = new AsyncTaskQueue
        {
            AutoCancelPreviousTask = true,
            UseSingleThread = true
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitData();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task InitData()
    {
        await LoadData();
        _projects = await PmClient.ProjectService.GetListAsync();
        _projectItems = _projects.Select(x => new SelectItem<string>(x.Identity, x.Name)).ToList();
        _metricItems = (await TscClient.MetricService.GetNamesAsync(null)).Select(x => new SelectItem<string>(x, x)).ToList();
    }

    private async Task LoadData()
    {
        Loading = true;
        _showEmptyPlaceholder = false;
        var result = await _asyncTaskQueue.ExecuteAsync(async () =>
        {
            var queryParam = _queryParam.Adapt<GetAlarmRuleInputDto>() ?? new();
            queryParam.StartTime = queryParam.StartTime?.Add(JsInitVariables.TimezoneOffset);
            queryParam.EndTime = queryParam.EndTime?.Add(JsInitVariables.TimezoneOffset);
            var dtos = (await AlarmRuleService.GetListAsync(queryParam));
            return dtos;
        });
        if (result.IsValid)
        {
            _entities = result.result?.Adapt<PaginatedListDto<AlarmRuleListViewModel>>() ?? new();
            Loading = false;
            _showEmptyPlaceholder = !_entities.Result.Any();
            StateHasChanged();
        }
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

    private async Task HandleDelAsync(Guid _entityId, string displayName)
    {
        await ConfirmAsync(T("DeletionConfirmationMessage", $"{T("AlarmRule")}\"{displayName}\""), async () => { await DeleteAsync(_entityId); }, AlertTypes.Error);
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
        if (_queryParam.Type == AlarmRuleTypes.Log)
        {
            await _logUpsertModal?.OpenModalAsync()!;
        }

        if (_queryParam.Type == AlarmRuleTypes.Metric)
        {
            await _metricUpsertModal?.OpenModalAsync()!;
        }
    }

    private async Task HandleEdit(AlarmRuleListViewModel item)
    {
        if (item.Type == AlarmRuleTypes.Log)
        {
            await _logUpsertModal?.OpenModalAsync(item)!;
        }

        if (item.Type == AlarmRuleTypes.Metric)
        {
            await _metricUpsertModal?.OpenModalAsync(item)!;
        }
    }

    private async Task HandleProjectChange()
    {
        await RefreshAsync();

        var projectId = _projects.FirstOrDefault(x => x.Identity == _queryParam.ProjectIdentity)?.Id;

        if (projectId == null)
        {
            return;
        }

        _appsItems = (await PmClient.AppService.GetListByProjectIdsAsync(new List<int> { projectId.Value })).Select(x => new SelectItem<string>(x.Identity, x.Name)).ToList();
    }

    private async Task HandleTypeChange()
    {
        _queryParam = new(20)
        {
            Type = _queryParam.Type
        };

        await RefreshAsync();
    }
}
