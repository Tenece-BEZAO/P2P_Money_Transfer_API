using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peer_to_peer_money_transfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditedAppUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TransactionHistoryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TransactionHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Complains",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_ApplicationUserId",
                table: "TransactionHistories",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ApplicationUserId",
                table: "Complains",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_AspNetUsers_ApplicationUserId",
                table: "Complains",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_ApplicationUserId",
                table: "TransactionHistories",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_AspNetUsers_ApplicationUserId",
                table: "Complains");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_AspNetUsers_ApplicationUserId",
                table: "TransactionHistories");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHistories_ApplicationUserId",
                table: "TransactionHistories");

            migrationBuilder.DropIndex(
                name: "IX_Complains_ApplicationUserId",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TransactionHistories");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Complains");

            migrationBuilder.AddColumn<long>(
                name: "ComplainsId",
                table: "AspNetUsers",
                type: "bigint",
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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TransactionHistories_TransactionHistoryId",
                table: "AspNetUsers",
                column: "TransactionHistoryId",
                principalTable: "TransactionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
