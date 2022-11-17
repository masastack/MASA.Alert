using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHistoryLastAlarmTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastNotificationTime",
                schema: "alert",
                table: "AlarmHistorys",
                type: "datetimeoffset",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRuleRecords_AlarmRules_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords");

            migrationBuilder.DropIndex(
                name: "IX_AlarmRuleRecords_AlarmRuleId",
                schema: "alert",
                table: "AlarmRuleRecords");

            migrationBuilder.DropColumn(
                name: "LastNotificationTime",
                schema: "alert",
                table: "AlarmHistorys");
        }
    }
}
