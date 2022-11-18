namespace Masa.Alert.EntityFrameworkCore;

public class AlertQueryContext : MasaDbContext, IAlertQueryContext
{
    public IQueryable<AlarmRuleQueryModel> AlarmRuleQueries => Set<AlarmRuleQueryModel>().AsQueryable();

    public IQueryable<AlarmRuleRecordQueryModel> AlarmRuleRecordQueries => Set<AlarmRuleRecordQueryModel>().AsQueryable();

    public IQueryable<AlarmHistoryQueryModel> AlarmHistoryQueries => Set<AlarmHistoryQueryModel>().AsQueryable();

    public AlertQueryContext(MasaDbContextOptions<AlertQueryContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.Entity<AlarmRuleQueryModel>(eb =>
        {
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmRules", AlertConsts.DbSchema);
        });

        builder.Entity<AlarmRuleRecordQueryModel>(eb =>
        {
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmRuleRecords", AlertConsts.DbSchema);
            eb.Property(x => x.AggregateResult).HasConversion(new JsonValueConverter<ConcurrentDictionary<string, long>>());
        });

        builder.Entity<AlarmHistoryQueryModel>(eb =>
        {
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmHistorys", AlertConsts.DbSchema);
            eb.HasOne(x => x.AlarmRule);
        });

        base.OnModelCreatingExecuting(builder);
    }
}
