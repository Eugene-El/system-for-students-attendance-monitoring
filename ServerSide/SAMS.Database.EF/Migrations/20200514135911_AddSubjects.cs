using Microsoft.EntityFrameworkCore.Migrations;

namespace SAMS.Database.EF.Migrations
{
    public partial class AddSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    TitleLv = table.Column<string>(nullable: true),
                    TitleRu = table.Column<string>(nullable: true),
                    TitleEn = table.Column<string>(nullable: true),
                    ShortTitleLv = table.Column<string>(nullable: true),
                    ShortTitleRu = table.Column<string>(nullable: true),
                    ShortTitleEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
