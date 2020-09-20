using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSystems",
                columns: table => new
                {
                    GameSystemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSystems", x => x.GameSystemId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    GameSystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId");
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    GameSystemId = table.Column<Guid>(nullable: false),
                    GameSystemId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId");
                    table.ForeignKey(
                        name: "FK_Races_GameSystems_GameSystemId1",
                        column: x => x.GameSystemId1,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    GameSystemId = table.Column<Guid>(nullable: false),
                    GameSystemId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId");
                    table.ForeignKey(
                        name: "FK_Roles_GameSystems_GameSystemId1",
                        column: x => x.GameSystemId1,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillGroups",
                columns: table => new
                {
                    SkillGroupId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    GameSystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillGroups", x => x.SkillGroupId);
                    table.ForeignKey(
                        name: "FK_SkillGroups_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Creatures",
                columns: table => new
                {
                    CreatureId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    GameSystemId = table.Column<Guid>(nullable: false),
                    RaceId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    Constitution = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Wisdom = table.Column<int>(nullable: false),
                    Charisma = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creatures", x => x.CreatureId);
                    table.ForeignKey(
                        name: "FK_Creatures_GameSystems_GameSystemId",
                        column: x => x.GameSystemId,
                        principalTable: "GameSystems",
                        principalColumn: "GameSystemId");
                    table.ForeignKey(
                        name: "FK_Creatures_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId");
                    table.ForeignKey(
                        name: "FK_Creatures_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SkillGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skills_SkillGroups_SkillGroupId",
                        column: x => x.SkillGroupId,
                        principalTable: "SkillGroups",
                        principalColumn: "SkillGroupId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    ItemInventoryId = table.Column<Guid>(nullable: false),
                    ItemId1 = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    CreatureId = table.Column<Guid>(nullable: true),
                    CreatureId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.ItemInventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "CreatureId");
                    table.ForeignKey(
                        name: "FK_InventoryItems_Creatures_CreatureId1",
                        column: x => x.CreatureId1,
                        principalTable: "Creatures",
                        principalColumn: "CreatureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_InventoryItems_Items_ItemId1",
                        column: x => x.ItemId1,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreatureSkills",
                columns: table => new
                {
                    CreatureSkillId = table.Column<Guid>(nullable: false),
                    CreatureId = table.Column<Guid>(nullable: false),
                    SkillId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureSkills", x => x.CreatureSkillId);
                    table.ForeignKey(
                        name: "FK_CreatureSkills_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "CreatureId");
                    table.ForeignKey(
                        name: "FK_CreatureSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_GameSystemId",
                table: "Creatures",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_RaceId",
                table: "Creatures",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_RoleId",
                table: "Creatures",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureSkills_CreatureId",
                table: "CreatureSkills",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureSkills_SkillId",
                table: "CreatureSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CreatureId",
                table: "InventoryItems",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CreatureId1",
                table: "InventoryItems",
                column: "CreatureId1");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemId",
                table: "InventoryItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemId1",
                table: "InventoryItems",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_GameSystemId",
                table: "Items",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_GameSystemId",
                table: "Races",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_GameSystemId1",
                table: "Races",
                column: "GameSystemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_GameSystemId",
                table: "Roles",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_GameSystemId1",
                table: "Roles",
                column: "GameSystemId1");

            migrationBuilder.CreateIndex(
                name: "IX_SkillGroups_GameSystemId",
                table: "SkillGroups",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillGroupId",
                table: "Skills",
                column: "SkillGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreatureSkills");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "SkillGroups");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "GameSystems");
        }
    }
}
