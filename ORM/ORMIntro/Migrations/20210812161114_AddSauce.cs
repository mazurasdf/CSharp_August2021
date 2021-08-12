using Microsoft.EntityFrameworkCore.Migrations;

namespace ORMIntro.Migrations
{
    public partial class AddSauce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sauce",
                table: "Sundaes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sauce",
                table: "Sundaes");
        }
    }
}
