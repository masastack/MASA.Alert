using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRuleOwnsOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilenceCycleValue",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_SilenceCycleValue");

            migrationBuilder.RenameColumn(
                name: "CronExpression",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_CronExpression");

            migrationBuilder.RenameColumn(
                name: "SilenceTimeValue",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_Type");

            migrationBuilder.RenameColumn(
                name: "SilenceTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_TimeInterval_IntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_TimeInterval_IntervalTime");

            migrationBuilder.RenameColumn(
                name: "CheckIntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_Type");

            migrationBuilder.RenameColumn(
                name: "CheckIntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_FixedInterval_IntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_FixedInterval_IntervalTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilenceCycle_SilenceCycleValue",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycleValue");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_CronExpression",
                schema: "alert",
                table: "AlarmRules",
                newName: "CronExpression");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_Type",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceTimeValue");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_TimeInterval_IntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceTimeType");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_TimeInterval_IntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_Type",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckIntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_FixedInterval_IntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckIntervalTime");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_FixedInterval_IntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency");
        }
    }
}
