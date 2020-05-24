using Microsoft.EntityFrameworkCore.Migrations;

namespace SAMS.Database.EF.Migrations
{
    public partial class ChangeNotificationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistory_Students_StudentId",
                table: "NotificationHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory");

            migrationBuilder.DropColumn(
                name: "AttendanceProcent",
                table: "NotificationHistory");

            migrationBuilder.RenameTable(
                name: "NotificationHistory",
                newName: "NotificationHistories");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationHistory_StudentId",
                table: "NotificationHistories",
                newName: "IX_NotificationHistories_StudentId");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "NotificationHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "NotificationHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationHistories",
                table: "NotificationHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistories_Students_StudentId",
                table: "NotificationHistories",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationHistories_Students_StudentId",
                table: "NotificationHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationHistories",
                table: "NotificationHistories");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "NotificationHistories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotificationHistories");

            migrationBuilder.RenameTable(
                name: "NotificationHistories",
                newName: "NotificationHistory");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationHistories_StudentId",
                table: "NotificationHistory",
                newName: "IX_NotificationHistory_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceProcent",
                table: "NotificationHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationHistory",
                table: "NotificationHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationHistory_Students_StudentId",
                table: "NotificationHistory",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
