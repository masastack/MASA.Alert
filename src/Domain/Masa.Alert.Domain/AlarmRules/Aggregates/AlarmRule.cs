namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class AlarmRule : FullAggregateRoot<Guid, Guid>
{
    public string DisplayName { get; protected set; } = string.Empty;

    public AlarmRuleTypes Type { get; protected set; }

    public string ProjectIdentity { get; protected set; } = string.Empty;

    public string AppIdentity { get; protected set; } = string.Empty;

    public bool IsEnabled { get; protected set; }

    public string ChartYAxisUnit { get; protected set; } = string.Empty;

    public CheckFrequency CheckFrequency { get; protected set; } = default!;

    public bool IsGetTotal { get; protected set; }

    public string TotalVariable { get; protected set; } = "total";

    public string WhereExpression { get; protected set; } = string.Empty;

    public int ContinuousTriggerThreshold { get; protected set; }

    public SilenceCycle SilenceCycle { get; protected set; } = default!;

    public List<LogMonitorItem> LogMonitorItems { get; protected set; } = new();

    public List<MetricMonitorItem> MetricMonitorItems { get; protected set; } = new();

    public ICollection<AlarmRuleItem> Items { get; protected set; } = new Collection<AlarmRuleItem>();

    public Guid SchedulerJobId { get; protected set; } = Guid.Empty;

    public string Source { get; set; } = default!;

    public bool Show { get; set; }

    private AlarmRule() { }

    public AlarmRule(string displayName, AlarmRuleTypes type, string projectIdentity, string appIdentity, string chartYAxisUnit
        , bool isGetTotal, string totalVariable, string whereExpression, int continuousTriggerThreshold, SilenceCycle silenceCycle,
        string source, bool show)
    {
        DisplayName = displayName;
        Type = type;
        ProjectIdentity = projectIdentity;
        AppIdentity = appIdentity;
        IsGetTotal = isGetTotal;
        TotalVariable = totalVariable;
        WhereExpression = whereExpression;
        Source = source;
        Show = show;

        SetChartConfig(chartYAxisUnit);
        SetAdvancedConfig(continuousTriggerThreshold, silenceCycle);
    }

    public string GetCronExpression()
    {
        return CheckFrequency.GetCronExpression();
    }

    public DateTimeOffset? GetStartCheckTime(DateTimeOffset checkTime, AlarmRuleRecord? latest)
    {
        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron && latest == null)
        {
            return null;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron)
        {
            return latest?.ExcuteTime;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.FixedInterval)
        {
            var intervalTime = CheckFrequency.FixedInterval.GetIntervalTime();
            return checkTime.Add(-intervalTime);
        }

        return null;
    }

    public DateTimeOffset? GetSilenceEndTime(DateTimeOffset lastNotificationTime)
    {
        if (SilenceCycle.Type == SilenceCycleTypes.Time)
        {
            var intervalTime = SilenceCycle.TimeInterval.GetIntervalTime();
            return lastNotificationTime.Add(intervalTime);
        }

        if (SilenceCycle.Type == SilenceCycleTypes.Cycle)
        {
            return GetSilenceEndTimeByCycle(lastNotificationTime);
        }

        return null;
    }

    public DateTimeOffset? GetSilenceEndTimeByCycle(DateTimeOffset lastTime)
    {
        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.FixedInterval)
        {
            var intervalTime = CheckFrequency.FixedInterval.GetIntervalTime();

            for (int i = 0; i < SilenceCycle.SilenceCycleValue; i++)
            {
                lastTime = lastTime.Add(intervalTime);
            }
            return lastTime;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron)
        {
            var cronExpression = new CronExpression(CheckFrequency.CronExpression);

            var timezone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

            if (timezone != null)
                cronExpression.TimeZone = timezone;

            for (int i = 0; i < SilenceCycle.SilenceCycleValue; i++)
            {
                var nextExcuteTime = cronExpression.GetNextValidTimeAfter(lastTime);
                if (nextExcuteTime.HasValue)
                {
                    lastTime = nextExcuteTime.Value;
                }
            }

            return lastTime;
        }

        return null;
    }

    public void CheckJob(bool isEnabled, CheckFrequency checkFrequency)
    {
        var oldCronExpression = Id != default ? CheckFrequency.GetCronExpression() : string.Empty;

        if (oldCronExpression != checkFrequency.GetCronExpression())
        {
            if (Id == default)
            {
                Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            }
            AddDomainEvent(new UpsertAlarmRuleJobEvent(Id));
        }

        if (isEnabled)
        {
            SetEnabled();
        }
        else
        {
            SetDisable();
        }

        SetCheckFrequency(checkFrequency);
    }

    public void SetEnabled()
    {
        if (!IsEnabled && SchedulerJobId != default)
        {
            AddDomainEvent(new EnableAlarmRuleJobEvent(Id));
        }

        IsEnabled = true;
    }

    public void SetDisable()
    {
        if (IsEnabled && SchedulerJobId != default)
        {
            AddDomainEvent(new DisableAlarmRuleJobEvent(Id));
        }

        IsEnabled = false;
    }

    public void SetChartConfig(string chartYAxisUnit)
    {
        ChartYAxisUnit = chartYAxisUnit;
    }

    public void SetAdvancedConfig(int continuousTriggerThreshold, SilenceCycle silenceCycle)
    {
        ContinuousTriggerThreshold = continuousTriggerThreshold;
        SilenceCycle = silenceCycle;
    }

    public void TriggerAlarm(DateTimeOffset excuteTime, ConcurrentDictionary<string, long> aggregateResult, List<RuleResultItem> ruleResult, int consecutiveCount)
    {
        var alertSeverity = ruleResult.Where(x => x.IsValid).Min(x => x.AlarmRuleItem.AlertSeverity);

        AddDomainEvent(new TriggerAlarmEvent(Id, alertSeverity, ruleResult, excuteTime, aggregateResult, consecutiveCount));
    }

    public void RecoveryAlarm(DateTimeOffset excuteTime, ConcurrentDictionary<string, long> aggregateResult, List<RuleResultItem> ruleResult)
    {
        AddDomainEvent(new RecoveryAlarmEvent(Id, excuteTime, aggregateResult, ruleResult));
    }

    public bool IsRuleValid(List<RuleResultItem> ruleResult)
    {
        return ruleResult.Exists(x => x.IsValid);
    }

    public bool CheckIsNotification()
    {
        return Items.Any(x => x.IsNotification);
    }

    public bool CheckIsSilence(DateTimeOffset? lastNotificationTime)
    {
        if (!lastNotificationTime.HasValue)
        {
            return false;
        }

        var silenceEndTime = GetSilenceEndTime(lastNotificationTime.Value);

        if (!silenceEndTime.HasValue || DateTimeOffset.Now > silenceEndTime)
        {
            return false;
        }

        return true;
    }

    public void SetCheckFrequency(CheckFrequency checkFrequency)
    {
        CheckFrequency = checkFrequency;
    }

    public void SetSchedulerJobId(Guid schedulerJobId)
    {
        SchedulerJobId = schedulerJobId;
    }
}
