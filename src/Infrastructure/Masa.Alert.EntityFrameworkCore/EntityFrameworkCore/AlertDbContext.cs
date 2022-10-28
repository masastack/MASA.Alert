namespace Masa.Alert.EntityFrameworkCore.EntityFrameworkCore;

public class AlertDbContext : IsolationDbContext
{
    public AlertDbContext(MasaDbContextOptions<AlertDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreatingExecuting(builder);
    }
}
