using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHandleStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandleStatus",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "alert",
                table: "AlarmHandleStatusCommits");

            migrationBuilder.DropColumn(
                name: "IsThirdParty",
                schema: "alert",
                table: "AlarmHandles");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "alert",
                table: "AlarmHandles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "alert",
                table: "AlarmHandles");

            migrationBuilder.AddColumn<int>(
                name: "HandleStatus",
                schema: "alert",
                table: "AlarmHistorys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "alert",
                table: "AlarmHandleStatusCommits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsThirdParty",
                schema: "alert",
                table: "AlarmHandles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
