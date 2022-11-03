// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Web.Admin.Pages.AlarmRules.Modules;

public partial class MetricAlarmRuleUpsertModal : AdminCompontentBase
{
    [Parameter]
    public EventCallback OnOk { get; set; }

    private MForm? _form;
    private AlarmRuleUpsertViewModel _model = new();
    private bool _visible;

    private bool _cronVisible;
    private string _tempCron = string.Empty;
    private string _nextRunTimeStr = string.Empty;
    private List<string> _items = new();
    private AlarmPreviewChartModal? _previewChart;

    protected override string? PageName { get; set; } = "AlarmRule";

    public async Task OpenModalAsync(AlarmRuleListViewModel? listModel = null)
    {
        _model = listModel?.Adapt<AlarmRuleUpsertViewModel>() ?? new();
        FillData();
        await InvokeAsync(() =>
        {
            _visible = true;
            StateHasChanged();
        });
    }

    private void FillData()
    {
        if (!_model.LogMonitorItems.Any())
        {
            _model.LogMonitorItems.Add(new LogMonitorItemViewModel());
        }
        if (!_model.MetricMonitorItems.Any())
        {
            _model.MetricMonitorItems.Add(new MetricMonitorItemViewModel());
        }
        if (!_model.Items.Any())
        {
            _model.Items.Add(new AlarmRuleItemViewModel());
        }
    }

    private void HandleCancel()
    {
        _visible = false;
        ResetForm();
    }

    private void ResetForm()
    {
        _model = new();
        _form?.ResetValidation();
    }

    private void HandleVisibleChanged(bool val)
    {
        if (!val) HandleCancel();
    }

    private async Task SetCronExpression()
    {
        if (CronExpression.IsValidExpression(_tempCron))
        {
            _model.CronExpression = _tempCron;
            GetNextRunTime();
            _cronVisible = false;
        }
        else
        {
            await PopupService.ToastErrorAsync(T("CronExpressionInvalid"));
        }
    }

    private void GetNextRunTime(int showCount = 5)
    {
        if (!CronExpression.IsValidExpression(_model.CronExpression))
        {
            _nextRunTimeStr = T("CronExpressionNotHasNextRunTime");
            return;
        }

        var sb = new StringBuilder();

        var startTime = DateTimeOffset.Now;

        var cronExpression = new CronExpression(_model.CronExpression);

        var timezone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(p => p.BaseUtcOffset == TimezoneOffset);

        if (timezone != null)
            cronExpression.TimeZone = timezone;

        for (int i = 0; i < showCount; i++)
        {
            var nextExcuteTime = cronExpression.GetNextValidTimeAfter(startTime);

            if (nextExcuteTime.HasValue)
            {
                startTime = nextExcuteTime.Value;
                sb.AppendLine(string.Format("<p>{0}</p>", startTime.ToOffset(TimezoneOffset).ToString("yyyy-MM-dd HH:mm:ss")));
            }
        }

        if (sb.Length == 0)
        {
            _nextRunTimeStr = T("CronExpressionNotHasNextRunTime");
        }
        else
        {
            _nextRunTimeStr = sb.ToString();
        }
    }

    private void OpenCronModal()
    {
        _cronVisible = true;
        _tempCron = _model.CronExpression;
    }

    private void HandleMetricMonitorItemsAdd()
    {
        _model.MetricMonitorItems.Add(new MetricMonitorItemViewModel());
    }

    private void HandleMetricMonitorItemsRemove(MetricMonitorItemViewModel item)
    {
        _model.MetricMonitorItems.Remove(item);
    }
}
