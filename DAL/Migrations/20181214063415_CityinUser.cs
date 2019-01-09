using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CityinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_City_Id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_City_Id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_City_Id",
                table: "Users",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
