using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class MetricMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aggregation_ComparisonOperator",
                schema: "alert",
                table: "AlarmRuleMetricMonitors",
                newName: "ComparisonOperator");

            migrationBuilder.RenameColumn(
                name: "Aggregation_AggregationType",
                schema: "alert",
                table: "AlarmRuleMetricMonitors",
                newName: "AggregationType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComparisonOperator",
                schema: "alert",
                table: "AlarmRuleMetricMonitors",
                newName: "Aggregation_ComparisonOperator");

            migrationBuilder.RenameColumn(
                name: "AggregationType",
                schema: "alert",
                table: "AlarmRuleMetricMonitors",
                newName: "Aggregation_AggregationType");
        }
    }
}
