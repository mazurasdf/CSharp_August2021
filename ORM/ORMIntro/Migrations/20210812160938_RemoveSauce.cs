using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMIntro.Migrations
{
    public partial class RemoveSauce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sauce",
                table: "Sundaes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sauce",
                table: "Sundaes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
