using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class AddLastUpdatedToMeasureAndActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                schema: "dbo",
                table: "Measures",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 10, 12, 41, 6, 155, DateTimeKind.Local).AddTicks(4726));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                schema: "dbo",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 10, 12, 41, 6, 200, DateTimeKind.Local).AddTicks(5910));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "dbo",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "dbo",
                table: "Activities");
        }
    }
}
