// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Service.Admin.Infrastructure.Extensions;

public static class MasaDbContextBuilderExtensions
{
    public static MasaDbContextBuilder UseDbSql(this MasaDbContextBuilder builder, string dbType)
    {
        switch (dbType)
        {
            case "PostgreSql":
                AlertDbContext.RegisterAssembly(typeof(AlertPostgreSqlDbContextFactory).Assembly);
                builder.UseNpgsql(b => b.MigrationsAssembly("Masa.Alert.EntityFrameworkCore.PostgreSql"));
                break;
            default:
                AlertDbContext.RegisterAssembly(typeof(AlertSqlServerDbContextFactory).Assembly);
                builder.UseSqlServer(b => b.MigrationsAssembly("Masa.Alert.EntityFrameworkCore.SqlServer"));
                break;
        }
        return builder;
    }
}
