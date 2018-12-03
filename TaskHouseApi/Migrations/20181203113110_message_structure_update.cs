using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHouseApi.Migrations
{
    public partial class message_structure_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Messages");
        }
    }
}
