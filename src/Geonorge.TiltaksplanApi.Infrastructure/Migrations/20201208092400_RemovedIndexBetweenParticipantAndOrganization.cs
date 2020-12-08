using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class RemovedIndexBetweenParticipantAndOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_OrganizationId",
                schema: "dbo",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_OrganizationId",
                schema: "dbo",
                table: "Participants",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_OrganizationId",
                schema: "dbo",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_OrganizationId",
                schema: "dbo",
                table: "Participants",
                column: "OrganizationId",
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");
        }
    }
}
