using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHandleMappingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHandles",
                schema: "alert");

            migrationBuilder.AddColumn<bool>(
                name: "HandleIsHandleNotice",
                schema: "alert",
                table: "AlarmHistorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HandleNotificationConfig",
                schema: "alert",
                table: "AlarmHistorys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HandleStatus",
                schema: "alert",
                table: "AlarmHistorys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Handler",
                schema: "alert",
                table: "AlarmHistorys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WebHookId",
                schema: "alert",
                table: "AlarmHistorys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandleIsHandleNotice",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.DropColumn(
                name: "HandleNotificationConfig",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.DropColumn(
                name: "HandleStatus",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.DropColumn(
                name: "Handler",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.DropColumn(
                name: "WebHookId",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.CreateTable(
                name: "AlarmHandles",
                schema: "alert",
                columns: table => new
                {
                    AlarmHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Handler = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHandleNotice = table.Column<bool>(type: "bit", nullable: false),
                    NotificationConfig = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WebHookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHandles", x => x.AlarmHistoryId);
                    table.ForeignKey(
                        name: "FK_AlarmHandles_AlarmHistorys_AlarmHistoryId",
                        column: x => x.AlarmHistoryId,
                        principalSchema: "alert",
                        principalTable: "AlarmHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
