using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peer_to_peer_money_transfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedUserEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "AspNetUsers",
                newName: "Verified");

            migrationBuilder.AddColumn<bool>(
                name: "Activated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BVN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CAC",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ComplainsId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lien",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NIN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionHistoryId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ComplainsId",
                table: "AspNetUsers",
                column: "ComplainsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TransactionHistoryId",
                table: "AspNetUsers",
                column: "TransactionHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers",
                column: "ComplainsId",
                principalTable: "Complains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TransactionHistories_TransactionHistoryId",
                table: "AspNetUsers",
                column: "TransactionHistoryId",
                principalTable: "TransactionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TransactionHistories_TransactionHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TransactionHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Activated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BVN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CAC",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lien",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NIN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TransactionHistoryId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Verified",
                table: "AspNetUsers",
                newName: "Active");
        }
    }
}
