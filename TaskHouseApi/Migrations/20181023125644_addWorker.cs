using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHouseApi.Migrations
{
    public partial class addWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Educations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_WorkerId",
                table: "Educations",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Users_WorkerId",
                table: "Educations",
                column: "WorkerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Users_WorkerId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_WorkerId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Educations");
        }
    }
}
