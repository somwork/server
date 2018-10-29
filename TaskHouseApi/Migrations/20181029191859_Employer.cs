using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHouseApi.Migrations
{
    public partial class Employer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployerId",
                table: "Tasks",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_EmployerId",
                table: "Tasks",
                column: "EmployerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_EmployerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EmployerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Tasks");
        }
    }
}
