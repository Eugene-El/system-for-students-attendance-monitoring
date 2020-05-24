using Microsoft.EntityFrameworkCore.Migrations;

namespace SAMS.Database.EF.Migrations
{
    public partial class AddNotificationRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NotificationMethod = table.Column<int>(nullable: false),
                    StudyProgrammeId = table.Column<int>(nullable: false),
                    Language = table.Column<int>(nullable: false),
                    LearningForm = table.Column<int>(nullable: false),
                    AttendanceProcent = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationRules_StudyProgrammes_StudyProgrammeId",
                        column: x => x.StudyProgrammeId,
                        principalTable: "StudyProgrammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRules_StudyProgrammeId",
                table: "NotificationRules",
                column: "StudyProgrammeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationRules");
        }
    }
}
