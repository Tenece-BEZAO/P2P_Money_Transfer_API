using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace peer_to_peer_money_transfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserEntity : Migration
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
                    { "31ad4c54-c8bc-4e84-a109-58fcad252042", true, "8137c9b7-1d67-4327-b6c4-3983204a4440", new DateTime(2023, 4, 6, 15, 1, 57, 227, DateTimeKind.Local).AddTicks(9027), "Administrator", null, null },
                    { "f1c25b75-5e26-4a0b-b2b8-b0a1360b18fa", true, "58d55d20-0eb2-42ac-a526-cebe879876fa", new DateTime(2023, 4, 6, 15, 1, 57, 227, DateTimeKind.Local).AddTicks(9049), "User", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountNumber", "Activated", "Address", "BVN", "Balance", "BusinessName", "BusinessType", "CAC", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Deleted", "Email", "EmailConfirmed", "FirstName", "LastName", "Lien", "LockoutEnabled", "LockoutEnd", "MiddleName", "NIN", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "RecoveryMail", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName", "UserTypeId", "Verified" },
                values: new object[] { "super-admin", 0, null, false, "cashmingleaddress", null, 0m, null, null, null, "aeeed802-54dd-4379-bcd9-806aeadec6ee", new DateTime(2023, 4, 6, 15, 1, 57, 227, DateTimeKind.Local).AddTicks(8493), null, false, "cashmingle@gmail.com", false, "cashMingleAdministrator", "cashMingleAdministrator", false, false, null, null, "12345678900", null, null, "123@Aa", "+2348080000000", false, null, null, "27f81e8e-35d3-49de-9501-1ec0c66c5845", false, null, "superAdministrator", 3, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ad4c54-c8bc-4e84-a109-58fcad252042");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1c25b75-5e26-4a0b-b2b8-b0a1360b18fa");

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
