using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskHouseApi.Persistence.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4);
        }
    }
}
