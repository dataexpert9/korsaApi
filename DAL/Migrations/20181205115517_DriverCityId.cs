using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DriverCityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "City_Id",
                table: "Drivers",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_City_Id",
                table: "Drivers",
                column: "City_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Cities_City_Id",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_City_Id",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "City_Id",
                table: "Drivers");
        }
    }
}
