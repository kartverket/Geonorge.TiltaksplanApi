using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class RemoveIndexBetweenMeasureAndOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Measures_OwnerId",
                schema: "dbo",
                table: "Measures");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_OwnerId",
                schema: "dbo",
                table: "Measures",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Measures_OwnerId",
                schema: "dbo",
                table: "Measures");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_OwnerId",
                schema: "dbo",
                table: "Measures",
                column: "OwnerId",
                unique: true);
        }
    }
}
