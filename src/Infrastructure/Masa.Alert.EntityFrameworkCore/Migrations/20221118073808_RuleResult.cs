using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class RuleResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TriggerRuleItems",
                schema: "alert",
                table: "AlarmRuleRecords",
                newName: "RuleResultItems");

            migrationBuilder.RenameColumn(
                name: "TriggerRuleItems",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "RuleResultItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuleResultItems",
                schema: "alert",
                table: "AlarmRuleRecords",
                newName: "TriggerRuleItems");

            migrationBuilder.RenameColumn(
                name: "RuleResultItems",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "TriggerRuleItems");
        }
    }
}
