// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmHistoryConfiguration : IEntityTypeConfiguration<AlarmHistory>
{
    public void Configure(EntityTypeBuilder<AlarmHistory> builder)
    {
        builder.ToTable(AlertConsts.DbTablePrefix + "AlarmHistorys", AlertConsts.DbSchema);
        builder.Property(x => x.RuleResultItems).HasConversion(new JsonValueConverter<List<RuleResultItem>>());
    }
}