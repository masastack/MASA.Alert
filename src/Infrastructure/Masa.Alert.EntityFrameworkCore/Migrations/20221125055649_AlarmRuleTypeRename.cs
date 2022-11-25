using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRuleTypeRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlarmRuleType",
                schema: "alert",
                table: "AlarmRules",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "alert",
                table: "AlarmRules",
                newName: "AlarmRuleType");
        }
    }
}
