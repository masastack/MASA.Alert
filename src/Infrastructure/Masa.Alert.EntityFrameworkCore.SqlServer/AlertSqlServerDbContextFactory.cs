// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore;

public class AlertSqlServerDbContextFactory : IDesignTimeDbContextFactory<AlertDbContext>
{
    public AlertDbContext CreateDbContext(string[] args)
    {
        AlertDbContext.RegisterAssembly(typeof(AlertSqlServerDbContextFactory).Assembly);
        var optionsBuilder = new MasaDbContextOptionsBuilder<AlertDbContext>();
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder
            .AddJsonFile("appsettings.SqlServer.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Masa.Alert.EntityFrameworkCore.SqlServer"));

        return new AlertDbContext(optionsBuilder.MasaOptions);
    }
}
