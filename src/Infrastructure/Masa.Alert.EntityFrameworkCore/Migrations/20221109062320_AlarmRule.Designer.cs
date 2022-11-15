﻿// <auto-generated />
using System;
using Masa.Alert.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(AlertDbContext))]
    [Migration("20221109062320_AlarmRule")]
    partial class AlarmRule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Masa.Alert.Domain.AlarmRules.AlarmRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppIdentity")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ChartYAxisUnit")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("CheckFrequency")
                        .HasColumnType("int");

                    b.Property<int>("CheckIntervalTime")
                        .HasColumnType("int");

                    b.Property<int>("CheckIntervalTimeType")
                        .HasColumnType("int");

                    b.Property<int>("ContinuousTriggerThreshold")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CronExpression")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGetTotal")
                        .HasColumnType("bit");

                    b.Property<string>("LogMonitorItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectIdentity")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("SilenceCycle")
                        .HasColumnType("int");

                    b.Property<int>("SilenceCycleValue")
                        .HasColumnType("int");

                    b.Property<int>("SilenceTimeType")
                        .HasColumnType("int");

                    b.Property<int>("SilenceTimeValue")
                        .HasColumnType("int");

                    b.Property<string>("TotalVariable")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("WhereExpression")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AlarmRules", "alert");
                });

            modelBuilder.Entity("Masa.Alert.Domain.AlarmRules.AlarmRuleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlarmRuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AlertSeverity")
                        .HasColumnType("int");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNotification")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecoveryNotification")
                        .HasColumnType("bit");

                    b.Property<string>("NotificationConfig")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecoveryNotificationConfig")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlarmRuleId");

                    b.ToTable("AlarmRuleItems", "alert");
                });

            modelBuilder.Entity("Masa.BuildingBlocks.Dispatcher.IntegrationEvents.Logs.IntegrationEventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("RowVersion");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "EventId", "RowVersion" }, "IX_EventId_Version");

                    b.HasIndex(new[] { "State", "ModificationTime" }, "IX_State_MTime");

                    b.HasIndex(new[] { "State", "TimesSent", "ModificationTime" }, "IX_State_TimesSent_MTime");

                    b.ToTable("IntegrationEventLog", (string)null);
                });

            modelBuilder.Entity("Masa.Alert.Domain.AlarmRules.AlarmRuleItem", b =>
                {
                    b.HasOne("Masa.Alert.Domain.AlarmRules.AlarmRule", null)
                        .WithMany("Items")
                        .HasForeignKey("AlarmRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Masa.Alert.Domain.AlarmRules.AlarmRule", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
