using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHouseApi.Migrations
{
    public partial class updated_offer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "complexity",
                table: "Offers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalHours",
                table: "Offers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "complexity",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "totalHours",
                table: "Offers");
        }
    }
}
