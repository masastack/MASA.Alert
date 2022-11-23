namespace Masa.Alert.EntityFrameworkCore;

public class AlertQueryContext : MasaDbContext, IAlertQueryContext
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
