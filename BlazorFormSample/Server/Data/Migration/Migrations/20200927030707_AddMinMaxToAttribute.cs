using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFormSample.Data.Migrations.Migrations
{
    public partial class AddMinMaxToAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Maximum",
                table: "Attributes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Minimum",
                table: "Attributes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ModifiedMaximum",
                table: "Attributes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ModifiedMinimum",
                table: "Attributes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Maximum",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Minimum",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ModifiedMaximum",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ModifiedMinimum",
                table: "Attributes");
        }
    }
}
