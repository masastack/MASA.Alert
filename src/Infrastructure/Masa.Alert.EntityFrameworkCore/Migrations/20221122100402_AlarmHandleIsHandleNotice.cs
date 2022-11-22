using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHandleIsHandleNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HandleIsHandleNotice",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "IsHandleNotice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHandleNotice",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "HandleIsHandleNotice");
        }
    }
}
