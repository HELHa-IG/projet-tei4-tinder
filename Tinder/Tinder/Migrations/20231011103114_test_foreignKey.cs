using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tinder.Migrations
{
    public partial class test_foreignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdLocality",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locality_IdLocality",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdLocality",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "IdLocality",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
