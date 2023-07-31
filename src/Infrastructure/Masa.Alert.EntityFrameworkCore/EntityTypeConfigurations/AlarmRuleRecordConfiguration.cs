// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleRecordConfiguration : IEntityTypeConfiguration<AlarmRuleRecord>
{
    public void Configure(EntityTypeBuilder<AlarmRuleRecord> builder)
    {
        builder.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmRuleRecords", AlertConsts.DB_SCHEMA);
        builder.Property(x => x.AggregateResult).HasConversion(new JsonValueConverter<ConcurrentDictionary<string, long>>());
        builder.Property(x => x.RuleResultItems).HasConversion(new JsonValueConverter<List<RuleResultItem>>());
    }
}