using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tinder.Migrations
{
    public partial class ForeignKeyUserLocality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users",
                column: "LocalityId",
                principalTable: "Locality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locality_LocalityId",
                table: "Users",
                column: "LocalityId",
                principalTable: "Locality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
