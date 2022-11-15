// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.EntityFrameworkCore.EntityTypeConfigurations;

public class AlarmRuleItemConfiguration : IEntityTypeConfiguration<AlarmRuleItem>
{
    public void Configure(EntityTypeBuilder<AlarmRuleItem> builder)
    {
        builder
            .ToTable(AlertConsts.DbTablePrefix + "AlarmRuleItems", AlertConsts.DbSchema);
        builder
            .Property(x => x.RecoveryNotificationConfig)
            .HasConversion(new JsonValueConverter<NotificationConfig>());
        builder
            .Property(x => x.NotificationConfig)
            .HasConversion(new JsonValueConverter<NotificationConfig>());
    }
}