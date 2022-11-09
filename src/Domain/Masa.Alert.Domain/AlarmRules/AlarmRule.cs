namespace Masa.Alert.Domain.AlarmRules;

public class AlarmRule : FullAggregateRoot<Guid, Guid>
{
    public string DisplayName { get; protected set; } = default!;

    public string ProjectIdentity { get; protected set; } = default!;

    public string AppIdentity { get; protected set; } = default!;

    public bool IsEnabled { get; protected set; }

    public string ChartYAxisUnit { get; protected set; } = default!;

    public AlarmCheckFrequencyTypes CheckFrequency { get; protected set; }

    public int CheckIntervalTime { get; protected set; }

    public TimeTypes CheckIntervalTimeType { get; protected set; } = TimeTypes.Minute;

    public string CronExpression { get; protected set; } = default!;

    public bool IsGetTotal { get; protected set; }

    public string TotalVariable { get; protected set; } = "total";

    public string WhereExpression { get; protected set; } = default!;

    public int ContinuousTriggerThreshold { get; protected set; }

    public AlarmRuleSilenceCycle SilenceCycle { get; protected set; }

    public int SilenceTimeValue { get; protected set; }

    public TimeTypes SilenceTimeType { get; protected set; } = TimeTypes.Minute;

    public int SilenceCycleValue { get; protected set; }

    public List<LogMonitorItem> LogMonitorItems { get; protected set; } = new();

    public ICollection<AlarmRuleItem> Items { get; protected set; } = new Collection<AlarmRuleItem>();

    public AlarmRule()
    {

    }

    public string GetCronExpression()
    {
        if (CheckFrequency == AlarmCheckFrequencyTypes.Cron)
        {
            return CronExpression;
        }

        if (CheckFrequency == AlarmCheckFrequencyTypes.FixedInterval)
        {
            switch (CheckIntervalTimeType)
            {
                case TimeTypes.Minute:
                    return $"* 0/{CheckIntervalTime} * * * ? ";
                case TimeTypes.Hour:
                    return $"* * 0/{CheckIntervalTime} * * ? ";
                case TimeTypes.Day:
                    return $"* * * 1/{CheckIntervalTime} * ? ";
                default:
                    return string.Empty;
            }
        }

        return string.Empty;
    }
}
