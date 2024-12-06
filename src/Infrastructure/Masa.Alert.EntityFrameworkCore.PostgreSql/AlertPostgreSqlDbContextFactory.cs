// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore;

public class AlertPostgreSqlDbContextFactory : IDesignTimeDbContextFactory<AlertDbContext>
{
    public AlertDbContext CreateDbContext(string[] args)
    {
        AlertDbContext.RegisterAssembly(typeof(AlertPostgreSqlDbContextFactory).Assembly);
        var optionsBuilder = new MasaDbContextOptionsBuilder<AlertDbContext>();
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder
            .AddJsonFile("appsettings.PostgreSql.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!, b => b.MigrationsAssembly("Masa.Alert.EntityFrameworkCore.PostgreSql"));

        return new AlertDbContext(optionsBuilder.MasaOptions);
    }
}
