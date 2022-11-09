// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRule>
{
    public void Configure(EntityTypeBuilder<AlarmRule> builder)
    {
        builder
            .ToTable(AlertConsts.DbTablePrefix + "AlarmRules", AlertConsts.DbSchema);
        builder
            .Property(x => x.DisplayName)
            .IsRequired()
            .HasMaxLength(128);
        builder
            .Property(x => x.ProjectIdentity)
            .HasMaxLength(128);
        builder
            .Property(x => x.AppIdentity)
            .HasMaxLength(128);
        builder
            .Property(x => x.ChartYAxisUnit)
            .HasMaxLength(128);
        builder
            .Property(x => x.CronExpression)
            .HasMaxLength(128);
        builder
            .Property(x => x.TotalVariable)
            .HasMaxLength(64);
        builder
            .Property(x => x.LogMonitorItems)
            .HasConversion(new JsonValueConverter<List<LogMonitorItem>>());
        builder
            .HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.AlarmRuleId)
            .IsRequired();
    }
}