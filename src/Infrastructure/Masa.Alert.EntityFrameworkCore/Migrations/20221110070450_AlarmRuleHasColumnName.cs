using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRuleHasColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilenceCycle_Type",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycleType");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_TimeInterval_IntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycleIntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_TimeInterval_IntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycleIntervalTime");

            migrationBuilder.RenameColumn(
                name: "SilenceCycle_SilenceCycleValue",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycleValue");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_Type",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequencyType");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_FixedInterval_IntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequencyIntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_FixedInterval_IntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequencyIntervalTime");

            migrationBuilder.RenameColumn(
                name: "CheckFrequency_CronExpression",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequencyCron");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilenceCycleValue",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_SilenceCycleValue");

            migrationBuilder.RenameColumn(
                name: "SilenceCycleType",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_Type");

            migrationBuilder.RenameColumn(
                name: "SilenceCycleIntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_TimeInterval_IntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "SilenceCycleIntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "SilenceCycle_TimeInterval_IntervalTime");

            migrationBuilder.RenameColumn(
                name: "CheckFrequencyType",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_Type");

            migrationBuilder.RenameColumn(
                name: "CheckFrequencyIntervalTimeType",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_FixedInterval_IntervalTimeType");

            migrationBuilder.RenameColumn(
                name: "CheckFrequencyIntervalTime",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_FixedInterval_IntervalTime");

            migrationBuilder.RenameColumn(
                name: "CheckFrequencyCron",
                schema: "alert",
                table: "AlarmRules",
                newName: "CheckFrequency_CronExpression");
        }
    }
}
