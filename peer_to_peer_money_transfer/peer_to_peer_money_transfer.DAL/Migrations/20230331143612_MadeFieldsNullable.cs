using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace peer_to_peer_money_transfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MadeFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ComplainsId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers",
                column: "ComplainsId",
                principalTable: "Complains",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ComplainsId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Complains_ComplainsId",
                table: "AspNetUsers",
                column: "ComplainsId",
                principalTable: "Complains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
