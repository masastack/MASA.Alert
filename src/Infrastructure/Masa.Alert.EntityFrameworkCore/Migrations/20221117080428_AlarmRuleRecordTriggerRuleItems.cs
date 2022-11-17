using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRuleRecordTriggerRuleItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlarmRuleItems",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "TriggerRuleItems");

            migrationBuilder.AddColumn<string>(
                name: "TriggerRuleItems",
                schema: "alert",
                table: "AlarmRuleRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsNotification",
                schema: "alert",
                table: "AlarmHistorys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerRuleItems",
                schema: "alert",
                table: "AlarmRuleRecords");

            migrationBuilder.DropColumn(
                name: "IsNotification",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.RenameColumn(
                name: "TriggerRuleItems",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "AlarmRuleItems");
        }
    }
}
