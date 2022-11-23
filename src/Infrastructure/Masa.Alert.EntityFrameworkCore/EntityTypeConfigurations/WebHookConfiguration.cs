// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Alert.Domain.WebHooks.Aggregates;

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class WebHookConfiguration : IEntityTypeConfiguration<WebHook>
{
    public void Configure(EntityTypeBuilder<WebHook> builder)
    {
        builder.ToTable(AlertConsts.DbTablePrefix + "WebHooks", AlertConsts.DbSchema);
    }
}