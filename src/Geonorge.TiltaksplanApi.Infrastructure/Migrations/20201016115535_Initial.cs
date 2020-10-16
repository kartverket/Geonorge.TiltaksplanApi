using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geonorge.TiltaksplanApi.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ActionPlans",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Progress = table.Column<string>(nullable: false),
                    Volume = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TrafficLight = table.Column<int>(nullable: false),
                    Results = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionPlanId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImplementationStart = table.Column<DateTime>(nullable: false),
                    ImplementationEnd = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActionPlans_ActionPlanId",
                        column: x => x.ActionPlanId,
                        principalSchema: "dbo",
                        principalTable: "ActionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "dbo",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActionPlanId",
                schema: "dbo",
                table: "Activities",
                column: "ActionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ActivityId",
                schema: "dbo",
                table: "Participants",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ActionPlans",
                schema: "dbo");
        }
    }
}
