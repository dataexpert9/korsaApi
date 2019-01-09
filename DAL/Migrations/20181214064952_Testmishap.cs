using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Testmishap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "City",
               table: "Users");
            
            migrationBuilder.AddColumn<int>(
                name: "City_Id",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_City_Id",
                table: "Users",
                column: "City_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_City_Id",
                table: "Users",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_City_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_City_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City_Id",
                table: "Users");
            
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                nullable: true);
        }
    }
}
