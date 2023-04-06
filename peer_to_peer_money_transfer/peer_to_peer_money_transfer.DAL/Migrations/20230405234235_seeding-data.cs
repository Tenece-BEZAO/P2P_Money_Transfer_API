using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace peer_to_peer_money_transfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "AspNetUsers",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "BVN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[,]
                {
                    { "ab1e2d52-ab68-43a6-8057-780a6a45b518", true, "28f9e430-50d0-42ff-bf47-30692fd63b72", new DateTime(2023, 4, 6, 0, 42, 34, 733, DateTimeKind.Local).AddTicks(2234), "Administrator", null, null },
                    { "e6b8bf46-c022-4f4a-a559-2349fc205b55", true, "1a88e3d6-1b76-4be9-af90-d49190f745ba", new DateTime(2023, 4, 6, 0, 42, 34, 733, DateTimeKind.Local).AddTicks(2286), "User", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountNumber", "Activated", "Address", "BVN", "Balance", "BusinessName", "BusinessType", "CAC", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Deleted", "Email", "EmailConfirmed", "FirstName", "LastName", "Lien", "LockoutEnabled", "LockoutEnd", "MiddleName", "NIN", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "RecoveryMail", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName", "UserTypeId", "Verified" },
                values: new object[] { "super-admin", 0, null, false, "cashmingleaddress", null, 0m, null, null, null, "c358837b-ee0c-4872-b13d-88e9ecb9cfcb", new DateTime(2023, 4, 6, 0, 42, 34, 733, DateTimeKind.Local).AddTicks(1747), null, false, "cashmingle@gmail.com", false, "cashMingleAdministrator", "cashMingleAdministrator", false, false, null, null, "12345678900", null, null, "123@Aa", "+2348080000000", false, null, null, "1bea66de-81bc-4a13-b204-f2933975d08a", false, null, "superAdministrator", 3, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab1e2d52-ab68-43a6-8057-780a6a45b518");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6b8bf46-c022-4f4a-a559-2349fc205b55");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "super-admin");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "AspNetUsers",
                newName: "Birthday");

            migrationBuilder.AlterColumn<string>(
                name: "BVN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
