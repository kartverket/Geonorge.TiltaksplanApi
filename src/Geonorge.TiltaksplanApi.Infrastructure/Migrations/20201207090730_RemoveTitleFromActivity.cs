using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class RemoveTitleFromActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "ActivityTranslations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "ActivityTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
