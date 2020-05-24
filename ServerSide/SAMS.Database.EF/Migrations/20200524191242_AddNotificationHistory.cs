using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAMS.Database.EF.Migrations
{
    public partial class AddNotificationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendancePeriod",
                table: "NotificationRules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendingTime = table.Column<DateTime>(nullable: false),
                    AttendanceProcent = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationHistory_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistory_StudentId",
                table: "NotificationHistory",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistory");

            migrationBuilder.DropColumn(
                name: "AttendancePeriod",
                table: "NotificationRules");
        }
    }
}
