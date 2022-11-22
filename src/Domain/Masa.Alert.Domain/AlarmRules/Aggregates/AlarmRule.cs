namespace Masa.Alert.Domain.AlarmRules.Aggregates;

public class AlarmRule : FullAggregateRoot<Guid, Guid>
{
    public string DisplayName { get; protected set; } = string.Empty;

    public AlarmRuleTypes AlarmRuleType { get; set; }

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

    public ICollection<AlarmRuleItem> Items { get; protected set; } = new Collection<AlarmRuleItem>();

    public virtual IEnumerable<AlarmRuleRecord> AlarmRuleRecords => LazyLoader.Load(this, ref _alarmRuleRecords!, nameof(AlarmRuleRecords))!;

    private List<AlarmRuleRecord> _alarmRuleRecords = default!;

    private Action<object, string> LazyLoader { get; set; } = default!;

    private AlarmRule(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

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
        _alarmRuleRecords = new List<AlarmRuleRecord>();
    }

    public AlarmRuleRecord? GetLatest()
    {
        LazyLoader.Load(this, ref _alarmRuleRecords!, nameof(AlarmRuleRecords));
        return _alarmRuleRecords.Where(x => x.AlarmRuleId == Id).OrderByDescending(x => x.CreationTime).FirstOrDefault();
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
        }

        return string.Empty;
    }

    public DateTime? GetStartCheckTime(DateTime checkTime, AlarmRuleRecord? latest)
    {
        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron && latest == null)
        {
            return null;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.Cron)
        {
            return latest?.CreationTime;
        }

        if (CheckFrequency.Type == AlarmCheckFrequencyTypes.FixedInterval)
        {
            var intervalTime = CheckFrequency.FixedInterval.GetIntervalTime();
            return intervalTime == null ? null : checkTime.Add(intervalTime.Value);
        }

        return null;
    }

    public DateTimeOffset? GetSilenceEndTime(DateTimeOffset lastNotificationTime)
    {
        if (SilenceCycle.Type == SilenceCycleTypes.Time)
        {
            var intervalTime = SilenceCycle.TimeInterval.GetIntervalTime();
            return intervalTime == null ? null : lastNotificationTime.Add(intervalTime.Value);
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

            if (intervalTime == null) return lastTime;

            for (int i = 0; i < SilenceCycle.SilenceCycleValue; i++)
            {
                lastTime = lastTime.Add(intervalTime.Value);
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

    public void SetEnabled()
    {
        IsEnabled = true;
    }

    public void SetDisable()
    {
        IsEnabled = false;
    }

    public void SetChartConfig(string chartYAxisUnit)
    {
        ChartYAxisUnit = chartYAxisUnit;
    }

    public void SetLogMonitorConfig(List<LogMonitorItem> items, bool isGetTotal, string totalVariable, string whereExpression)
    {
        LogMonitorItems = items;
        IsGetTotal = isGetTotal;
        TotalVariable = totalVariable;
        WhereExpression = whereExpression;
    }

    public void SetAdvancedConfig(int continuousTriggerThreshold, SilenceCycle silenceCycle)
    {
        ContinuousTriggerThreshold = continuousTriggerThreshold;
        SilenceCycle = silenceCycle;
    }

    public void Check(ConcurrentDictionary<string, long> aggregateResult, List<RuleResultItem> ruleResult)
    {
        var latestRecord = GetLatest();
        var consecutiveCount = latestRecord?.ConsecutiveCount ?? 0;
        var isTrigger = ruleResult.Any(x=>x.IsValid);

        if (isTrigger)
        {
            consecutiveCount++;
        }
        else
        {
            consecutiveCount = 0;
        }

        _alarmRuleRecords.Add(new AlarmRuleRecord(Id, aggregateResult, isTrigger, consecutiveCount, ruleResult));

        if (isTrigger && consecutiveCount >= ContinuousTriggerThreshold)
        {
            var alertSeverity = ruleResult.Where(x=>x.IsValid).Min(x => x.AlarmRuleItem.AlertSeverity);

            AddDomainEvent(new TriggerAlarmEvent(Id, alertSeverity, ruleResult));
        }
        else if (latestRecord != null && latestRecord.IsTrigger)
        {
            AddDomainEvent(new RecoveryAlarmEvent(Id));
        }
    }

    public void SkipCheck()
    {
        _alarmRuleRecords.Add(new AlarmRuleRecord(Id, new ConcurrentDictionary<string, long>(), false, 0, new List<RuleResultItem>()));
    }

    public bool CheckIsNotification(DateTimeOffset? lastNotificationTime)
    {
        if (!Items.Any(x => x.IsNotification))
        {
            return false;
        }

        if (!lastNotificationTime.HasValue)
        {
            return true;
        }

        var silenceEndTime = GetSilenceEndTime(lastNotificationTime.Value);

        if (DateTimeOffset.Now > silenceEndTime)
        {
            return true;
        }

        return false;
    }
}
