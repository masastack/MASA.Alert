using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class LogMetricMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogMonitorItems",
                schema: "alert",
                table: "AlarmRules");

            migrationBuilder.DropColumn(
                name: "MetricMonitorItems",
                schema: "alert",
                table: "AlarmRules");

            migrationBuilder.CreateTable(
                name: "AlarmRuleLogMonitors",
                schema: "alert",
                columns: table => new
                {
                    AlarmRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggregationType = table.Column<int>(type: "int", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOffset = table.Column<bool>(type: "bit", nullable: false),
                    OffsetPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleLogMonitors", x => new { x.AlarmRuleId, x.Id });
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
                    AlarmRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsExpression = table.Column<bool>(type: "bit", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aggregation_ComparisonOperator = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aggregation_AggregationType = table.Column<int>(type: "int", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOffset = table.Column<bool>(type: "bit", nullable: false),
                    OffsetPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleMetricMonitors", x => new { x.AlarmRuleId, x.Id });
                    table.ForeignKey(
                        name: "FK_AlarmRuleMetricMonitors_AlarmRules_AlarmRuleId",
                        column: x => x.AlarmRuleId,
                        principalSchema: "alert",
                        principalTable: "AlarmRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmRuleLogMonitors",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleMetricMonitors",
                schema: "alert");

            migrationBuilder.AddColumn<string>(
                name: "LogMonitorItems",
                schema: "alert",
                table: "AlarmRules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetricMonitorItems",
                schema: "alert",
                table: "AlarmRules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
