using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class AddedNoAsUniqueIndexForMeasureAndActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Measures_No",
                schema: "dbo",
                table: "Measures",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MeasureId_No",
                schema: "dbo",
                table: "Activities",
                columns: new[] { "MeasureId", "No" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Measures_No",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Activities_MeasureId_No",
                schema: "dbo");
        }
    }
}
