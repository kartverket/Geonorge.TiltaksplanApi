using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class RemoveResponsibleAgencyFromActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Organizations_ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities",
                column: "ResponsibleAgencyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Organizations_ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities",
                column: "ResponsibleAgencyId",
                principalSchema: "dbo",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}
