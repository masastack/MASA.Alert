// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using System.Linq.Expressions;
using Nest;

namespace Masa.Alert.Web.Admin.Pages.AlarmHistory.Modules;

public partial class HandleAlarmModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private bool _visible;
    private Guid _entityId;
    private AlarmHistoryViewModel _model = new();
    private string _tab = "";
    private List<string> _items = new();
    private bool _isThirdParty;

    AlarmHistoryService AlarmHistoryService => AlertCaller.AlarmHistoryService;

    protected override string? PageName { get; set; } = "AlarmHistory";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _tab = T("AlarmDetails");
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task OpenModalAsync(AlarmHistoryListViewModel? listModel = null)
    {
        _entityId = listModel?.Id ?? default;
        _model = listModel?.Adapt<AlarmHistoryViewModel>() ?? new();

        if (_entityId != default)
        {
            await GetFormDataAsync();
        }

        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });
    }

    private async Task GetFormDataAsync()
    {
        var dto = await AlarmHistoryService.GetAsync(_entityId) ?? new();
        _model = dto.Adapt<AlarmHistoryViewModel>();
        //_model.AlarmRule.Type = AlarmRuleTypes.Metric;
        //    _model.AlarmRule.LogMonitorItems = new List<LogMonitorItemViewModel>();

        //_model.AlarmRule.MetricMonitorItems.Add(new MetricMonitorItemViewModel
        //{
        //    IsExpression = true,
        //    Expression = "count(get_token_fail_count{ack_aliyun_com = \"c439ca51366374c0091b13f5179149389\"})",
        //    Aggregation= new MetricAggregationViewModel { },
        //    Alias="b",
        //    IsOffset=true,
        //    OffsetPeriod=4
        //});
        //_model.AlarmRule.MetricMonitorItems.Add(new MetricMonitorItemViewModel
        //{
        //    IsExpression = false,
        //    Expression = "",
        //    Aggregation = new MetricAggregationViewModel {
        //        Name= "alertmanager_cluster_members",
        //        Tag= "instance",
        //        ComparisonOperator= MetricComparisonOperator.Equal.Id,
        //        Value= "alertmanager:9093",
        //        AggregationType= MetricAggregationTypes.Count.Id
        //    },
        //    Alias = "a",
        //    IsOffset = true,
        //    OffsetPeriod = 4
        //});
    }

    private void HandleCancel()
    {
        _visible = false;
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private async void HandleAlarm()
    {
        Loading = true;
        var inputDto = _model.Handle.Adapt<AlarmHandleDto>();
        await AlarmHistoryService.HandleAsync(_entityId, inputDto);
        Loading = false;
        _visible = false;
        await SuccessMessageAsync(T("OperationSuccessfulMessage"));

        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }

    private void SelectAlarmHandle(bool isThirdParty)
    {
        _isThirdParty = isThirdParty;

        if (!isThirdParty)
        {
            _model.Handle.WebHookId = default;
        }
    }

    private async Task HandleDel()
    {
        await ConfirmAsync(T("DeletionConfirmationMessage"), DeleteAsync);
    }

    private async Task DeleteAsync()
    {
        Loading = true;
        await AlarmHistoryService.DeleteAsync(_entityId);
        Loading = false;
        await SuccessMessageAsync(T("DeletedSuccessfullyMessage"));
        _visible = false;
        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
    }
}
