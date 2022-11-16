// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRule>
{
    public void Configure(EntityTypeBuilder<AlarmRule> builder)
    {
        builder.ToTable(AlertConsts.DbTablePrefix + "AlarmRules", AlertConsts.DbSchema);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.ProjectIdentity).HasMaxLength(128);
        builder.Property(x => x.AppIdentity).HasMaxLength(128);
        builder.Property(x => x.ChartYAxisUnit).HasMaxLength(128);
        builder.Property(x => x.TotalVariable).HasMaxLength(64);
        builder.Property(x => x.LogMonitorItems).HasConversion(new JsonValueConverter<List<LogMonitorItem>>());
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.AlarmRuleId).IsRequired();
        builder.HasMany(x => x.AlarmRuleRecords).WithOne();
        builder.OwnsOne(x => x.CheckFrequency, y =>
        {
            y.OwnsOne(z => z.FixedInterval, z =>
            {
                z.Property(z => z.IntervalTime).HasColumnName("CheckFrequencyIntervalTime");
                z.Property(z => z.IntervalTimeType).HasColumnName("CheckFrequencyIntervalTimeType");
            });
            y.Property(z => z.CronExpression).HasMaxLength(128).HasColumnName("CheckFrequencyCron");
            y.Property(z => z.Type).HasColumnName("CheckFrequencyType");
            
        });
        builder.OwnsOne(x => x.SilenceCycle, y =>
        {
            y.OwnsOne(z => z.TimeInterval, z =>
            {
                z.Property(z => z.IntervalTime).HasColumnName("SilenceCycleIntervalTime");
                z.Property(z => z.IntervalTimeType).HasColumnName("SilenceCycleIntervalTimeType");
            });
            y.Property(z => z.SilenceCycleValue).HasColumnName("SilenceCycleValue");
            y.Property(z => z.Type).HasColumnName("SilenceCycleType");
        });
    }
}