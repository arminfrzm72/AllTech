using Microsoft.EntityFrameworkCore.Migrations;

namespace AllTech.DataLayer.Migrations
{
    public partial class Correct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Wallets",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserID",
                table: "Wallets",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserID",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Wallets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserID",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
