using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonationApp.Data.Migrations
{
    public partial class newm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "b9987362-6f01-4132-84e1-56d1f1443e2d", "2a14e69f-42ae-42af-b144-af0b3f799e01" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9987362-6f01-4132-84e1-56d1f1443e2d");

            migrationBuilder.AddColumn<string>(
                name: "AdminCenterId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminCenterId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BloodGroup", "ConcurrencyStamp", "DonatedTimesCount", "Email", "EmailConfirmed", "FirstName", "LastName", "LastTimeDonated", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Town", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b9987362-6f01-4132-84e1-56d1f1443e2d", 0, null, "b5d50f84-7fc6-41b1-818a-081794caf880", 0, "admin@blood.com", false, "Admin1", "Admin1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, "AQAAAAEAACcQAAAAEE5J7P9eScNOrdlLg3W9gg+FvoveSm1EJfl162xVFtPL6OmnjKWWQxJGeSpBKUE47g==", null, false, null, "Blagoevgrad", false, "Admin1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "b9987362-6f01-4132-84e1-56d1f1443e2d", "2a14e69f-42ae-42af-b144-af0b3f799e01" });
        }
    }
}
