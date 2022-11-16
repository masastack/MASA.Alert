namespace Masa.Alert.EntityFrameworkCore;

public class AlertQueryContext : MasaDbContext, IAlertQueryContext
{
    public IQueryable<AlarmRuleRecordQuery> AlarmRuleRecordQueries => Set<AlarmRuleRecordQuery>().AsQueryable();

    public AlertQueryContext(MasaDbContextOptions<AlertQueryContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.Entity<AlarmRuleRecordQuery>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(AlertConsts.DbTablePrefix + "AlarmRuleRecords", AlertConsts.DbSchema);
        });

        base.OnModelCreatingExecuting(builder);
    }
}
