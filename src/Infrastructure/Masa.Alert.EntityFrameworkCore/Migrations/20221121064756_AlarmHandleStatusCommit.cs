using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHandleStatusCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "HandleStatus");

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                schema: "alert",
                table: "AlarmHistorys",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AlarmHandleStatusCommits",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlarmHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHandleStatusCommits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmHandleStatusCommits_AlarmHistorys_AlarmHistoryId",
                        column: x => x.AlarmHistoryId,
                        principalSchema: "alert",
                        principalTable: "AlarmHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HandleNotifications",
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
                    table.PrimaryKey("PK_HandleNotifications", x => x.AlarmHistoryId);
                    table.ForeignKey(
                        name: "FK_HandleNotifications_AlarmHistorys_AlarmHistoryId",
                        column: x => x.AlarmHistoryId,
                        principalSchema: "alert",
                        principalTable: "AlarmHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmHandleStatusCommits_AlarmHistoryId",
                schema: "alert",
                table: "AlarmHandleStatusCommits",
                column: "AlarmHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHandleStatusCommits",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "HandleNotifications",
                schema: "alert");

            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "alert",
                table: "AlarmHistorys");

            migrationBuilder.RenameColumn(
                name: "HandleStatus",
                schema: "alert",
                table: "AlarmHistorys",
                newName: "Status");
        }
    }
}
