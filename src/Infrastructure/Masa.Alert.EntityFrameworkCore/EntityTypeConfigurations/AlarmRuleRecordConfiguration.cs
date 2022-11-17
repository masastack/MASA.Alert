// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleRecordConfiguration : IEntityTypeConfiguration<AlarmRuleRecord>
{
    public void Configure(EntityTypeBuilder<AlarmRuleRecord> builder)
    {
        builder.ToTable(AlertConsts.DbTablePrefix + "AlarmRuleRecords", AlertConsts.DbSchema);
        builder.Property(x => x.AggregateResult).HasConversion(new JsonValueConverter<ConcurrentDictionary<string, long>>());
    }
}