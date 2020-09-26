using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    public partial class AddAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    GameSystemId1 = table.Column<Guid>(nullable: true),
                    GameSystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId");
                    table.ForeignKey(
                        name: "FK_Attributes_GameSystems_GameSystemId1",
                        column: x => x.GameSystemId1,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceSkillModifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SkillId = table.Column<Guid>(nullable: false),
                    Modifier = table.Column<decimal>(nullable: false),
                    RaceId = table.Column<Guid>(nullable: false),
                    RaceId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSkillModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSkillModifiers_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceSkillModifiers_Races_RaceId1",
                        column: x => x.RaceId1,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceSkillModifiers_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceAttributeModifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttributeId = table.Column<Guid>(nullable: false),
                    Modifier = table.Column<decimal>(nullable: false),
                    RaceId = table.Column<Guid>(nullable: false),
                    RaceId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceAttributeModifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceAttributeModifiers_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceAttributeModifiers_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceAttributeModifiers_Races_RaceId1",
                        column: x => x.RaceId1,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_GameSystemId",
                table: "Attributes",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_GameSystemId1",
                table: "Attributes",
                column: "GameSystemId1");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAttributeModifiers_AttributeId",
                table: "RaceAttributeModifiers",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAttributeModifiers_RaceId",
                table: "RaceAttributeModifiers",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceAttributeModifiers_RaceId1",
                table: "RaceAttributeModifiers",
                column: "RaceId1");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSkillModifiers_RaceId",
                table: "RaceSkillModifiers",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSkillModifiers_RaceId1",
                table: "RaceSkillModifiers",
                column: "RaceId1");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSkillModifiers_SkillId",
                table: "RaceSkillModifiers",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceAttributeModifiers");

            migrationBuilder.DropTable(
                name: "RaceSkillModifiers");

            migrationBuilder.DropTable(
                name: "Attributes");
        }
    }
}
