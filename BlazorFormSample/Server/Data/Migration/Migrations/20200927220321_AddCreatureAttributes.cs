using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    public partial class AddCreatureAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charisma",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Constitution",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Dexterity",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "Wisdom",
                table: "Creatures");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RaceSkillModifiers",
                newName: "RaceSkillModifierId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RaceAttributeModifiers",
                newName: "RaceAttributeModifierId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attributes",
                newName: "AttributeId");

            migrationBuilder.AddColumn<decimal>(
                name: "Order",
                table: "Attributes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CreatureAttributes",
                columns: table => new
                {
                    CreatureAttribute = table.Column<Guid>(nullable: false),
                    CreatureId = table.Column<Guid>(nullable: false),
                    AttributeId = table.Column<Guid>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureAttributes", x => x.CreatureAttribute);
                    table.ForeignKey(
                        name: "FK_CreatureAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "AttributeId");
                    table.ForeignKey(
                        name: "FK_CreatureAttributes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "CreatureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreatureAttributes_AttributeId",
                table: "CreatureAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureAttributes_CreatureId",
                table: "CreatureAttributes",
                column: "CreatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreatureAttributes");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Attributes");

            migrationBuilder.RenameColumn(
                name: "RaceSkillModifierId",
                table: "RaceSkillModifiers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RaceAttributeModifierId",
                table: "RaceAttributeModifiers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "Attributes",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Charisma",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Constitution",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wisdom",
                table: "Creatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
