namespace Masa.Alert.EntityFrameworkCore;

public class AlertDbContext : MasaDbContext<AlertDbContext>
{
    internal static Assembly Assembly = typeof(AlertDbContext).Assembly;

    public AlertDbContext(MasaDbContextOptions<AlertDbContext> options) : base(options)
    {
        base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore.IntegrationEventLogModelCreatingProvider).Assembly);
        builder.ApplyConfigurationsFromAssembly(Assembly);
        base.OnModelCreatingExecuting(builder);
    }

    protected override void OnConfiguring(MasaDbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.DbContextOptionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    public static void RegisterAssembly(Assembly assembly)
    {
        Assembly = assembly;
    }
}
