using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "alert");

            migrationBuilder.CreateTable(
                name: "AlarmRules",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProjectIdentity = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AppIdentity = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ChartYAxisUnit = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CheckFrequency = table.Column<int>(type: "int", nullable: false),
                    CheckIntervalTime = table.Column<int>(type: "int", nullable: false),
                    CheckIntervalTimeType = table.Column<int>(type: "int", nullable: false),
                    CronExpression = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsGetTotal = table.Column<bool>(type: "bit", nullable: false),
                    TotalVariable = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    WhereExpression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinuousTriggerThreshold = table.Column<int>(type: "int", nullable: false),
                    SilenceCycle = table.Column<int>(type: "int", nullable: false),
                    SilenceTimeValue = table.Column<int>(type: "int", nullable: false),
                    SilenceTimeType = table.Column<int>(type: "int", nullable: false),
                    SilenceCycleValue = table.Column<int>(type: "int", nullable: false),
                    LogMonitorItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleItems",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertSeverity = table.Column<int>(type: "int", nullable: false),
                    IsRecoveryNotification = table.Column<bool>(type: "bit", nullable: false),
                    IsNotification = table.Column<bool>(type: "bit", nullable: false),
                    RecoveryNotificationConfig = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationConfig = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRuleItems_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleItems",
                column: "AlarmRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmRuleItems",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRules",
                schema: "alert");
        }
    }
}
