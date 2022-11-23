using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHandleNotifications",
                schema: "alert");

            migrationBuilder.CreateTable(
                name: "AlarmHandles",
                schema: "alert",
                columns: table => new
                {
                    AlarmHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsThirdParty = table.Column<bool>(type: "bit", nullable: false),
                    Handler = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebHookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHandleNotice = table.Column<bool>(type: "bit", nullable: false),
                    NotificationConfig = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHandles",
                schema: "alert");

            migrationBuilder.CreateTable(
                name: "AlarmHandleNotifications",
                schema: "alert",
                columns: table => new
                {
                    AlarmHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Handler = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHandleNotice = table.Column<bool>(type: "bit", nullable: false),
                    IsThirdParty = table.Column<bool>(type: "bit", nullable: false),
                    NotificationConfig = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebHookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHandleNotifications", x => x.AlarmHistoryId);
                    table.ForeignKey(
                        name: "FK_AlarmHandleNotifications_AlarmHistorys_AlarmHistoryId",
                        column: x => x.AlarmHistoryId,
                        principalSchema: "alert",
                        principalTable: "AlarmHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
