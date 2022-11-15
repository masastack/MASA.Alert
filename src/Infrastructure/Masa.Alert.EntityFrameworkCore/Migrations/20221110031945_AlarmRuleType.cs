using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmRuleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlarmRuleType",
                schema: "alert",
                table: "AlarmRules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmRuleType",
                schema: "alert",
                table: "AlarmRules");
        }
    }
}
