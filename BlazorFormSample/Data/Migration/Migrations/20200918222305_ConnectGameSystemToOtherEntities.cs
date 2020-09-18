using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    public partial class ConnectGameSystemToOtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameSystemId",
                table: "Items",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GameSystemId",
                table: "Creatures",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GameSystem",
                columns: table => new
                {
                    GameSystemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSystem", x => x.GameSystemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_GameSystemId",
                table: "Items",
                column: "GameSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_GameSystemId",
                table: "Creatures",
                column: "GameSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creatures_GameSystem_GameSystemId",
                table: "Creatures",
                column: "GameSystemId",
                principalTable: "GameSystem",
                principalColumn: "GameSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_GameSystem_GameSystemId",
                table: "Items",
                column: "GameSystemId",
                principalTable: "GameSystem",
                principalColumn: "GameSystemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creatures_GameSystem_GameSystemId",
                table: "Creatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_GameSystem_GameSystemId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "GameSystem");

            migrationBuilder.DropIndex(
                name: "IX_Items_GameSystemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Creatures_GameSystemId",
                table: "Creatures");

            migrationBuilder.DropColumn(
                name: "GameSystemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GameSystemId",
                table: "Creatures");
        }
    }
}
