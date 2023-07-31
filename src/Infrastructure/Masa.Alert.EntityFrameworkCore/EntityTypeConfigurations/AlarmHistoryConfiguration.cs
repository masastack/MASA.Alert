// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmHistoryConfiguration : IEntityTypeConfiguration<AlarmHistory>
{
    public void Configure(EntityTypeBuilder<AlarmHistory> builder)
    {
        builder.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmHistorys", AlertConsts.DB_SCHEMA);
        builder.Property(x => x.RuleResultItems).HasConversion(new JsonValueConverter<List<RuleResultItem>>());
        builder.OwnsMany(x => x.HandleStatusCommits, b =>
        {
            b.ToTable(AlertConsts.DB_TABLE_PREFIX + "AlarmHandleStatusCommits", AlertConsts.DB_SCHEMA);
            b.Property<Guid>("Id").ValueGeneratedOnAdd();
            b.HasKey("Id");
        });
        builder.OwnsOne(x => x.Handle, b =>
        {
            //b.ToTable(AlertConsts.DbTablePrefix + "AlarmHandles", AlertConsts.DbSchema);
            b.Property(x => x.Handler).HasColumnName("Handler");
            b.Property(x => x.WebHookId).HasColumnName("WebHookId");
            b.Property(x => x.Status).HasColumnName("HandleStatus");
            b.Property(x => x.IsHandleNotice).HasColumnName("IsHandleNotice");
            b.Property(x => x.NotificationConfig).HasConversion(new JsonValueConverter<NotificationConfig>()).HasColumnName("HandleNotificationConfig");
        });
    }
}