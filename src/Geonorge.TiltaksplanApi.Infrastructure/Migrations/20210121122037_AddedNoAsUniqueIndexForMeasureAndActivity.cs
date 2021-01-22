using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class AddedNoAsUniqueIndexForMeasureAndActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Measures",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Measures_No",
                schema: "dbo",
                table: "Measures",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_No",
                schema: "dbo",
                table: "Activities",
                column: "No",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Measures_No",
                schema: "dbo",
                table: "Measures");

            migrationBuilder.DropIndex(
                name: "IX_Activities_No",
                schema: "dbo",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Measures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "No",
                schema: "dbo",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));
        }
    }
}
