namespace Masa.Alert.EntityFrameworkCore;

public class AlertDbContext : MasaDbContext<AlertDbContext>
{
    public AlertDbContext(MasaDbContextOptions<AlertDbContext> options) : base(options)
    {
        base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore.IntegrationEventLogModelCreatingProvider).Assembly);
        base.OnModelCreatingExecuting(builder);
    }

    protected override void OnConfiguring(MasaDbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.DbContextOptionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}
