using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Alert.EntityFrameworkCore.Migrations
{
    public partial class AlarmHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmHistorys",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlertSeverity = table.Column<int>(type: "int", nullable: false),
                    FirstAlarmTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AlarmCount = table.Column<int>(type: "int", nullable: false),
                    LastAlarmTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecoveryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AlarmRuleItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRuleRecords",
                schema: "alert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTrigger = table.Column<bool>(type: "bit", nullable: false),
                    ConsecutiveCount = table.Column<int>(type: "int", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRuleRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHistorys",
                schema: "alert");

            migrationBuilder.DropTable(
                name: "AlarmRuleRecords",
                schema: "alert");
        }
    }
}
