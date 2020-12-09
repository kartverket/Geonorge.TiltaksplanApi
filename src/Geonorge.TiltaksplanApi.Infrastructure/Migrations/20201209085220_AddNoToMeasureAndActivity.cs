using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class AddNoToMeasureAndActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Measures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Activities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                schema: "dbo",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "No",
                schema: "dbo",
                table: "Activities");
        }
    }
}
