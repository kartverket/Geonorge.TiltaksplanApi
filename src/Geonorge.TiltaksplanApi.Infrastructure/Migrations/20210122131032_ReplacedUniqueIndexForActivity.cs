using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class ReplacedUniqueIndexForActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Activities_No",
                schema: "dbo",
                table: "Activities");

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
                name: "IX_Activities_MeasureId_No",
                schema: "dbo",
                table: "Activities");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_No",
                schema: "dbo",
                table: "Activities",
                column: "No",
                unique: true);
        }
    }
}
