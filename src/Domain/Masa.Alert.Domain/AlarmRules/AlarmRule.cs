namespace Masa.Alert.Domain.AlarmRules;

public class AlarmRule : FullAggregateRoot<Guid, Guid>
{
    public string DisplayName { get; protected set; } = default!;

    public AlarmRuleTypes AlarmRuleType { get; set; }

    public string ProjectIdentity { get; protected set; } = default!;

    public string AppIdentity { get; protected set; } = default!;

    public bool IsEnabled { get; protected set; }

    public string ChartYAxisUnit { get; protected set; } = default!;

    public CheckFrequency CheckFrequency { get; protected set; } = default!;

    public bool IsGetTotal { get; protected set; }

    public string TotalVariable { get; protected set; } = "total";

    public string WhereExpression { get; protected set; } = default!;

    public int ContinuousTriggerThreshold { get; protected set; }

    public SilenceCycle SilenceCycle { get; protected set; } = default!;

    public List<LogMonitorItem> LogMonitorItems { get; protected set; } = new();

    public ICollection<AlarmRuleItem> Items { get; protected set; } = new Collection<AlarmRuleItem>();

    private AlarmRule() { }

    public AlarmRule(string displayName, AlarmRuleTypes alarmRuleType, string projectIdentity, string appIdentity, bool isEnabled, string chartYAxisUnit
        , CheckFrequency checkFrequency, bool isGetTotal, string totalVariable, string whereExpression, int continuousTriggerThreshold, SilenceCycle silenceCycle)
    {
        DisplayName = displayName;
        AlarmRuleType = alarmRuleType;
        ProjectIdentity = projectIdentity;
        AppIdentity = appIdentity;
        IsEnabled = isEnabled;
        IsGetTotal = isGetTotal;
        TotalVariable = totalVariable;
        WhereExpression = whereExpression;

        SetChartConfig(chartYAxisUnit);
        CheckFrequency = checkFrequency;
        SetAdvancedConfig(continuousTriggerThreshold, silenceCycle);
    }

    public string GetCronExpression()
    {
        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron)
        {
            return CheckFrequency.CronExpression;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.FixedInterval)
        {
            throw new NotImplementedException();
            //switch (CheckFrequency.FixedInterval.IntervalTimeType)
            //{
            //    case TimeTypes.Minute:
            //        return $"* 0/{CheckFrequency.FixedInterval.IntervalTime} * * * ? ";
            //    case TimeTypes.Hour:
            //        return $"* * 0/{CheckFrequency.FixedInterval.IntervalTime} * * ? ";
            //    case TimeTypes.Day:
            //        return $"* * * 1/{CheckFrequency.FixedInterval.IntervalTime} * ? ";
            //    default:
            //        return string.Empty;
            //}
        }

        return string.Empty;
    }

    public void SetChartConfig(string chartYAxisUnit)
    {
        ChartYAxisUnit = chartYAxisUnit;
    }

    //public void SetCheckFrequency(AlarmCheckFrequencyTypes checkFrequencyType, int checkIntervalTime, TimeTypes checkIntervalTimeType, string cronExpression)
    //{
    //    if (checkFrequencyType == AlarmCheckFrequencyTypes.FixedInterval)
    //    {
    //        var fixedInterval = new TimeInterval(checkIntervalTime, checkIntervalTimeType);
    //        CheckFrequency = new CheckFrequency(fixedInterval);
    //    }

    //    if (checkFrequencyType == AlarmCheckFrequencyTypes.Cron)
    //    {
    //        CheckFrequency = new CheckFrequency(cronExpression);
    //    }
    //}

    public void SetLogMonitorConfig(List<LogMonitorItem> items, bool isGetTotal, string totalVariable, string whereExpression)
    {
        LogMonitorItems = items;
        IsGetTotal = isGetTotal;
        TotalVariable = totalVariable;
        WhereExpression = whereExpression;
    }

    public void SetTriggerRules(ICollection<AlarmRuleItem> items)
    {
        Items = items;
    }

    public void SetAdvancedConfig(int continuousTriggerThreshold, SilenceCycle silenceCycle)
    {
        ContinuousTriggerThreshold = continuousTriggerThreshold;
        SilenceCycle = silenceCycle;
    }
}
