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
            eb.HasNoKey();
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmRules", AlertConsts.DbSchema);
        });

        builder.Entity<AlarmRuleRecordQueryModel>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmRuleRecords", AlertConsts.DbSchema);
        });

        builder.Entity<AlarmHistoryQueryModel>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmHistorys", AlertConsts.DbSchema);
        });

        base.OnModelCreatingExecuting(builder);
    }
}
