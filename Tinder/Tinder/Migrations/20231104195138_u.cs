using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tinder.Migrations
{
    public partial class u : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User01Id",
                table: "MatchLike",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User02Id",
                table: "MatchLike",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchLike_User01Id",
                table: "MatchLike",
                column: "User01Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchLike_User02Id",
                table: "MatchLike",
                column: "User02Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLike_Users_User01Id",
                table: "MatchLike",
                column: "User01Id",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLike_Users_User02Id",
                table: "MatchLike",
                column: "User02Id",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchLike_Users_User01Id",
                table: "MatchLike");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchLike_Users_User02Id",
                table: "MatchLike");

            migrationBuilder.DropIndex(
                name: "IX_MatchLike_User01Id",
                table: "MatchLike");

            migrationBuilder.DropIndex(
                name: "IX_MatchLike_User02Id",
                table: "MatchLike");

            migrationBuilder.DropColumn(
                name: "User01Id",
                table: "MatchLike");

            migrationBuilder.DropColumn(
                name: "User02Id",
                table: "MatchLike");
        }
    }
}
