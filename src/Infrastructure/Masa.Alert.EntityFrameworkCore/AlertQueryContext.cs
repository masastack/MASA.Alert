namespace Masa.Alert.EntityFrameworkCore;

public class AlertQueryContext : MasaDbContext<AlertQueryContext>, IAlertQueryContext
{
    public IQueryable<AlarmRuleQueryModel> AlarmRuleQueries => Set<AlarmRuleQueryModel>().AsQueryable();

    public IQueryable<AlarmRuleRecordQueryModel> AlarmRuleRecordQueries => Set<AlarmRuleRecordQueryModel>().AsQueryable();

    public IQueryable<AlarmHistoryQueryModel> AlarmHistoryQueries => Set<AlarmHistoryQueryModel>().AsQueryable();

    public IQueryable<WebHookQueryModel> WebHookQueries => Set<WebHookQueryModel>().AsQueryable();

    public AlertQueryContext(MasaDbContextOptions<AlertQueryContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.Entity<AlarmRuleQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmRules", AlertConsts.DbSchema);
            b.HasMany(x => x.LogMonitorItems).WithOne().HasForeignKey(x => x.AlarmRuleId).IsRequired();
            b.HasMany(x => x.MetricMonitorItems).WithOne().HasForeignKey(x => x.AlarmRuleId).IsRequired();
            b.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.AlarmRuleId).IsRequired();
            b.OwnsOne(x => x.CheckFrequency, b =>
            {
                b.OwnsOne(x => x.FixedInterval, b =>
                {
                    b.Property(x => x.IntervalTime).HasColumnName("CheckFrequencyIntervalTime");
                    b.Property(x => x.IntervalTimeType).HasColumnName("CheckFrequencyIntervalTimeType");
                });
                b.Property(x => x.CronExpression).HasMaxLength(128).HasColumnName("CheckFrequencyCron");
                b.Property(x => x.Type).HasColumnName("CheckFrequencyType");

            });
            b.OwnsOne(x => x.SilenceCycle, b =>
            {
                b.OwnsOne(x => x.TimeInterval, b =>
                {
                    b.Property(x => x.IntervalTime).HasColumnName("SilenceCycleIntervalTime");
                    b.Property(x => x.IntervalTimeType).HasColumnName("SilenceCycleIntervalTimeType");
                });
                b.Property(x => x.SilenceCycleValue).HasColumnName("SilenceCycleValue");
                b.Property(x => x.Type).HasColumnName("SilenceCycleType");
            });
        });

        builder.Entity<AlarmRuleRecordQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmRuleRecords", AlertConsts.DbSchema);
            b.Property(x => x.AggregateResult).HasConversion(new JsonValueConverter<ConcurrentDictionary<string, long>>());
            b.Property(x => x.RuleResultItems).HasConversion(new JsonValueConverter<List<RuleResultItemQueryModel>>());
        });

        builder.Entity<AlarmHistoryQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmHistorys", AlertConsts.DbSchema);
            b.HasOne(x => x.AlarmRule);
            b.Property(x => x.HandleNotificationConfig).HasConversion(new JsonValueConverter<NotificationConfigQueryModel>());
            b.Property(x => x.RuleResultItems).HasConversion(new JsonValueConverter<List<RuleResultItemQueryModel>>());
            b.HasMany(x => x.HandleStatusCommits).WithOne().HasForeignKey(x => x.AlarmHistoryId).IsRequired();
        });

        builder.Entity<LogMonitorItemQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmRuleLogMonitors", AlertConsts.DbSchema);
        });

        builder.Entity<MetricMonitorItemQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmRuleMetricMonitors", AlertConsts.DbSchema);
            b.OwnsOne(x => x.Aggregation, b =>
            {
                b.Property(x => x.Name).HasColumnName("Name");
                b.Property(x => x.Tag).HasColumnName("Tag");
                b.Property(x => x.Value).HasColumnName("Value");
                b.Property(x => x.ComparisonOperator).HasColumnName("ComparisonOperator");
                b.Property(x => x.AggregationType).HasColumnName("AggregationType");
            });
        });

        builder.Entity<AlarmRuleItemQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmRuleItems", AlertConsts.DbSchema);
            b.Property(x => x.RecoveryNotificationConfig).HasConversion(new JsonValueConverter<NotificationConfigQueryModel>());
            b.Property(x => x.NotificationConfig).HasConversion(new JsonValueConverter<NotificationConfigQueryModel>());
        });

        builder.Entity<AlarmHandleStatusCommitQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "AlarmHandleStatusCommits", AlertConsts.DbSchema);
        });

        builder.Entity<WebHookQueryModel>(b =>
        {
            b.ToView(AlertConsts.DbTablePrefix + "WebHooks", AlertConsts.DbSchema);
        });

        base.OnModelCreatingExecuting(builder);
    }
}
