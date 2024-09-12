// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRule>
{
    public void Configure(EntityTypeBuilder<AlarmRule> builder)
    {
        builder.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmRules", AlertConsts.DB_SCHEMA);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.ProjectIdentity).HasMaxLength(128);
        builder.Property(x => x.AppIdentity).HasMaxLength(128);
        builder.Property(x => x.ChartYAxisUnit).HasMaxLength(128);
        builder.Property(x => x.TotalVariable).HasMaxLength(64);
        builder.Property(x => x.Source).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Show).HasDefaultValue(true).IsRequired();
        builder.OwnsMany(x => x.LogMonitorItems, b =>
        {
            b.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmRuleLogMonitors", AlertConsts.DB_SCHEMA);
            b.Property<Guid>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
            b.Property(x => x.AggregationType).HasConversion(v => v.Id, v => Enumeration.FromValue<Domain.AlarmRules.LogAggregationType>(v));
        });
        builder.OwnsMany(x => x.MetricMonitorItems, b =>
        {
            b.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmRuleMetricMonitors", AlertConsts.DB_SCHEMA);
            b.Property<Guid>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
            b.OwnsOne(x => x.Aggregation, b =>
            {
                b.Property(x => x.Name).HasColumnName("Name");
                b.Property(x => x.Tag).HasColumnName("Tag");
                b.Property(x => x.Value).HasColumnName("Value");
                b.Property(x => x.ComparisonOperator).HasConversion(v => v.Id, v => Enumeration.FromValue<MetricComparisonOperator>(v)).HasColumnName("ComparisonOperator");
                b.Property(x => x.AggregationType).HasConversion(v => v.Id, v => Enumeration.FromValue<MetricAggregationType>(v)).HasColumnName("AggregationType");
            });
        });
        builder.OwnsMany(x => x.Items, b =>
        {
            b.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmRuleItems", AlertConsts.DB_SCHEMA);
            b.Property<Guid>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
            b.Property(x => x.RecoveryNotificationConfig).HasConversion(new JsonValueConverter<NotificationConfig>());
            b.Property(x => x.NotificationConfig).HasConversion(new JsonValueConverter<NotificationConfig>());
        });
        builder.OwnsOne(x => x.CheckFrequency, b =>
        {
            b.OwnsOne(x => x.FixedInterval, b =>
            {
                b.Property(x => x.IntervalTime).HasColumnName("CheckFrequencyIntervalTime");
                b.Property(x => x.IntervalTimeType).HasConversion(x => x.Id, x => Enumeration.FromValue<TimeType>(x)).HasColumnName("CheckFrequencyIntervalTimeType");
            });
            b.Property(x => x.CronExpression).HasMaxLength(128).HasColumnName("CheckFrequencyCron");
            b.Property(x => x.Type).HasColumnName("CheckFrequencyType");

        });
        builder.OwnsOne(x => x.SilenceCycle, b =>
        {
            b.OwnsOne(x => x.TimeInterval, b =>
            {
                b.Property(x => x.IntervalTime).HasColumnName("SilenceCycleIntervalTime");
                b.Property(x => x.IntervalTimeType).HasConversion(x => x.Id, x => Enumeration.FromValue<TimeType>(x)).HasColumnName("SilenceCycleIntervalTimeType");
            });
            b.Property(x => x.SilenceCycleValue).HasColumnName("SilenceCycleValue");
            b.Property(x => x.Type).HasColumnName("SilenceCycleType");
        });
    }
}