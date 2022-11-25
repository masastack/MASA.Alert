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
        builder.Property(x => x.MetricMonitorItems).HasConversion(new JsonValueConverter<List<MetricMonitorItem>>());
        builder.OwnsMany(x => x.Items, b =>
        {
            b.ToTable(AlertConsts.DbTablePrefix + "AlarmRuleItems", AlertConsts.DbSchema);
            b.Property<Guid>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
            b.Property(x => x.RecoveryNotificationConfig).HasConversion(new JsonValueConverter<NotificationConfig>());
            b.Property(x => x.NotificationConfig).HasConversion(new JsonValueConverter<NotificationConfig>());
        });
        builder.HasMany(x => x.AlarmRuleRecords).WithOne();
        builder.OwnsOne(x => x.CheckFrequency, b =>
        {
            b.OwnsOne(x => x.FixedInterval, b =>
            {
                b.Property(x => x.IntervalTime).HasColumnName("CheckFrequencyIntervalTime");
                b.Property(x => x.IntervalTimeType).HasColumnName("CheckFrequencyIntervalTimeType");
            });
            b.Property(x => x.CronExpression).HasMaxLength(128).HasColumnName("CheckFrequencyCron");
            b.Property(x => x.Type).HasColumnName("CheckFrequencyType");
            
        });
        builder.OwnsOne(x => x.SilenceCycle, b =>
        {
            b.OwnsOne(x => x.TimeInterval, b =>
            {
                b.Property(x => x.IntervalTime).HasColumnName("SilenceCycleIntervalTime");
                b.Property(x => x.IntervalTimeType).HasColumnName("SilenceCycleIntervalTimeType");
            });
            b.Property(x => x.SilenceCycleValue).HasColumnName("SilenceCycleValue");
            b.Property(x => x.Type).HasColumnName("SilenceCycleType");
        });
    }
}