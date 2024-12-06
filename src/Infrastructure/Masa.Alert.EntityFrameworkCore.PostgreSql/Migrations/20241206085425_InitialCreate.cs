using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.PostgreSql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "alert");

            migrationBuilder.CreateTable(
                name: "AlarmHistorys",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlertSeverity = table.Column<int>(type: "integer", nullable: false),
                    FirstAlarmTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AlarmCount = table.Column<int>(type: "integer", nullable: false),
                    LastAlarmTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RecoveryTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastNotificationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    IsNotification = table.Column<bool>(type: "boolean", nullable: false),
                    RuleResultItems = table.Column<string>(type: "text", nullable: false),
                    Handler = table.Column<Guid>(type: "uuid", nullable: false),
                    WebHookId = table.Column<Guid>(type: "uuid", nullable: false),
                    HandleStatus = table.Column<int>(type: "integer", nullable: false),
                    IsHandleNotice = table.Column<bool>(type: "boolean", nullable: false),
                    HandleNotificationConfig = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleRecords",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateResult = table.Column<string>(type: "text", nullable: false),
                    IsTrigger = table.Column<bool>(type: "boolean", nullable: false),
                    ConsecutiveCount = table.Column<int>(type: "integer", nullable: false),
                    ExcuteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RuleResultItems = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRules",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ProjectIdentity = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AppIdentity = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ChartYAxisUnit = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CheckFrequencyType = table.Column<int>(type: "integer", nullable: false),
                    CheckFrequencyIntervalTime = table.Column<int>(type: "integer", nullable: false),
                    CheckFrequencyIntervalTimeType = table.Column<int>(type: "integer", nullable: false),
                    CheckFrequencyCron = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsGetTotal = table.Column<bool>(type: "boolean", nullable: false),
                    TotalVariable = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    WhereExpression = table.Column<string>(type: "text", nullable: false),
                    ContinuousTriggerThreshold = table.Column<int>(type: "integer", nullable: false),
                    SilenceCycleType = table.Column<int>(type: "integer", nullable: false),
                    SilenceCycleIntervalTime = table.Column<int>(type: "integer", nullable: false),
                    SilenceCycleIntervalTimeType = table.Column<int>(type: "integer", nullable: false),
                    SilenceCycleValue = table.Column<int>(type: "integer", nullable: false),
                    SchedulerJobId = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false, defaultValue: ""),
                    Show = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ExpandContent = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebHooks",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SecretKey = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmHandleStatusCommits",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    AlarmHistoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHandleStatusCommits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmHandleStatusCommits_AlarmHistorys_AlarmHistoryId",
                        column: x => x.AlarmHistoryId,
                        principalSchema: "alert",
                        principalTable: "AlarmHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleItems",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Expression = table.Column<string>(type: "text", nullable: false),
                    AlertSeverity = table.Column<int>(type: "integer", nullable: false),
                    IsRecoveryNotification = table.Column<bool>(type: "boolean", nullable: false),
                    IsNotification = table.Column<bool>(type: "boolean", nullable: false),
                    RecoveryNotificationConfig = table.Column<string>(type: "text", nullable: false),
                    NotificationConfig = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRuleItems_AlarmRules_AlarmRuleId",
                        column: x => x.AlarmRuleId,
                        principalSchema: "alert",
                        principalTable: "AlarmRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleLogMonitors",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    AggregationType = table.Column<int>(type: "integer", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    IsOffset = table.Column<bool>(type: "boolean", nullable: false),
                    OffsetPeriod = table.Column<int>(type: "integer", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleLogMonitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRuleLogMonitors_AlarmRules_AlarmRuleId",
                        column: x => x.AlarmRuleId,
                        principalSchema: "alert",
                        principalTable: "AlarmRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleMetricMonitors",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsExpression = table.Column<bool>(type: "boolean", nullable: false),
                    Expression = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    ComparisonOperator = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AggregationType = table.Column<int>(type: "integer", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    IsOffset = table.Column<bool>(type: "boolean", nullable: false),
                    OffsetPeriod = table.Column<int>(type: "integer", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleMetricMonitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRuleMetricMonitors_AlarmRules_AlarmRuleId",
                        column: x => x.AlarmRuleId,
                        principalSchema: "alert",
                        principalTable: "AlarmRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmHandleStatusCommits_AlarmHistoryId",
                schema: "alert",
                table: "AlarmHandleStatusCommits",
                column: "AlarmHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRuleItems_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleItems",
                column: "AlarmRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRuleLogMonitors_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleLogMonitors",
                column: "AlarmRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRuleMetricMonitors_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleMetricMonitors",
                column: "AlarmRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventId_Version",
                table: "IntegrationEventLog",
                columns: new[] { "EventId", "RowVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_State_MTime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "ModificationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_State_TimesSent_MTime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "TimesSent", "ModificationTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHandleStatusCommits",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleItems",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleLogMonitors",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleMetricMonitors",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleRecords",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DropTable(
                name: "WebHooks",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmHistorys",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRules",
                schema: "alert");
        }
    }
}
