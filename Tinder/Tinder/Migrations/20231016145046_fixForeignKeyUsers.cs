using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tinder.Migrations
{
    public partial class fixForeignKeyUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locality_IdLocality",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdLocality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdLocality",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocalityId",
                table: "Users",
                column: "LocalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users",
                column: "LocalityId",
                principalTable: "Locality",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocalityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "IdLocality",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdLocality",
                table: "Users",
                column: "IdLocality");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locality_IdLocality",
                table: "Users",
                column: "IdLocality",
                principalTable: "Locality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
