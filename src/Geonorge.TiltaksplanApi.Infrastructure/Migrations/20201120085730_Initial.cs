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
                name: "Languages",
                schema: "dbo",
                columns: table => new
                {
                    Culture = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Culture);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    OrgNumber = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(nullable: false),
                    Volume = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    TrafficLight = table.Column<int>(nullable: true),
                    Results = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measures_Organizations_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dbo",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasureId = table.Column<int>(nullable: false),
                    ResponsibleAgencyId = table.Column<int>(nullable: false),
                    ImplementationStart = table.Column<DateTime>(nullable: false),
                    ImplementationEnd = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalSchema: "dbo",
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Organizations_ResponsibleAgencyId",
                        column: x => x.ResponsibleAgencyId,
                        principalSchema: "dbo",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeasureTranslations",
                schema: "dbo",
                columns: table => new
                {
                    MeasureId = table.Column<int>(nullable: false),
                    LanguageCulture = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Progress = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureTranslations", x => new { x.MeasureId, x.LanguageCulture });
                    table.ForeignKey(
                        name: "FK_MeasureTranslations_Languages_LanguageCulture",
                        column: x => x.LanguageCulture,
                        principalSchema: "dbo",
                        principalTable: "Languages",
                        principalColumn: "Culture",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureTranslations_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalSchema: "dbo",
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTranslations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LanguageCulture = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTranslations_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "dbo",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityTranslations_Languages_LanguageCulture",
                        column: x => x.LanguageCulture,
                        principalSchema: "dbo",
                        principalTable: "Languages",
                        principalColumn: "Culture",
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
                    OrganizationId = table.Column<int>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Participants_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "dbo",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MeasureId",
                schema: "dbo",
                table: "Activities",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ResponsibleAgencyId",
                schema: "dbo",
                table: "Activities",
                column: "ResponsibleAgencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTranslations_ActivityId",
                schema: "dbo",
                table: "ActivityTranslations",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTranslations_LanguageCulture",
                schema: "dbo",
                table: "ActivityTranslations",
                column: "LanguageCulture");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_OwnerId",
                schema: "dbo",
                table: "Measures",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasureTranslations_LanguageCulture",
                schema: "dbo",
                table: "MeasureTranslations",
                column: "LanguageCulture");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ActivityId",
                schema: "dbo",
                table: "Participants",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_OrganizationId",
                schema: "dbo",
                table: "Participants",
                column: "OrganizationId",
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTranslations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MeasureTranslations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Participants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Measures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "dbo");
        }
    }
}
