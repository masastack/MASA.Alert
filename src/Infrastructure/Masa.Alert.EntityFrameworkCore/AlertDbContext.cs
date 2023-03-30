using Microsoft.EntityFrameworkCore;

namespace Masa.Alert.EntityFrameworkCore;

public class AlertDbContext : MasaDbContext<AlertDbContext>
{
    public AlertDbContext(MasaDbContextOptions<AlertDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreatingExecuting(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}
