using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaskHouseApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    From = table.Column<decimal>(nullable: false),
                    To = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    PrimaryLine = table.Column<string>(nullable: true),
                    SecondaryLine = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Text = table.Column<string>(nullable: true),
                    SendAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Token = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Urgency = table.Column<string>(nullable: true),
                    EmployerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "Country", "PrimaryLine", "SecondaryLine", "ZipCode" },
                values: new object[,]
                {
                    { -1, "City1", "Country1", "PrimaryLine1", "SecondaryLine1", "ZipCode1" },
                    { -2, "City2", "Country2", "PrimaryLine2", "SecondaryLine2", "ZipCode2" },
                    { -3, "City3", "Country3", "PrimaryLine3", "SecondaryLine3", "ZipCode3" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { -1, "Skill1" },
                    { -2, "Skill2" },
                    { -3, "Skill3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "FirstName", "LastName", "Password", "Salt", "Username" },
                values: new object[,]
                {
                    { -4, "Employer", "root@root.com", "em1", "emsen1", "mxurWhuDuXFA6EMY11qsixSbftITzPbpOtBU+Kbdr6Q=", "HplteyrRxcNz6bOoiZi4Qw==", "em1" },
                    { -5, "Employer", "test@test.com", "em2", "emsen2", "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", "upYKQSsrlub5JAID61/6pA==", "em2" },
                    { -6, "Employer", "test@test.com", "em3", "emsen3", "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", "U+cUJhQU56X+OCiGF9hb1g==", "em3" },
                    { -1, "Worker", "root@root.com", "Bob", "Bobsen", "mxurWhuDuXFA6EMY11qsixSbftITzPbpOtBU+Kbdr6Q=", "HplteyrRxcNz6bOoiZi4Qw==", "root" },
                    { -2, "Worker", "test@test.com", "Bob1", "Bobsen1", "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", "upYKQSsrlub5JAID61/6pA==", "1234" },
                    { -3, "Worker", "test@test.com", "Bob3", "Bobsen3", "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", "U+cUJhQU56X+OCiGF9hb1g==", "hej" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Deadline", "Description", "EmployerId", "Start", "Urgency" },
                values: new object[,]
                {
                    { -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task1", -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { -2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task2", -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task4", -4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { -3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Task3", -5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployerId",
                table: "Tasks",
                column: "EmployerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
