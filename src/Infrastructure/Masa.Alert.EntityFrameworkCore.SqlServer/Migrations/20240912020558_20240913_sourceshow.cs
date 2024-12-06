using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class _20240913_sourceshow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRuleRecords_AlarmRules_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords");

            migrationBuilder.DropIndex(
                name: "IX_AlarmRuleRecords_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords");

            migrationBuilder.AddColumn<bool>(
                name: "Show",
                schema: "alert",
                table: "AlarmRules",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                schema: "alert",
                table: "AlarmRules",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Show",
                schema: "alert",
                table: "AlarmRules");

            migrationBuilder.DropColumn(
                name: "Source",
                schema: "alert",
                table: "AlarmRules");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRuleRecords_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords",
                column: "AlarmRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmRuleRecords_AlarmRules_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords",
                column: "AlarmRuleId",
                principalSchema: "alert",
                principalTable: "AlarmRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
