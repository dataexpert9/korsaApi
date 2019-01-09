using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FareCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareCalculations_Cities_CityId",
                table: "FareCalculations");

            migrationBuilder.DropIndex(
                name: "IX_FareCalculations_CityId",
                table: "FareCalculations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "FareCalculations");

            migrationBuilder.CreateIndex(
                name: "IX_FareCalculations_City_Id",
                table: "FareCalculations",
                column: "City_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FareCalculations_Cities_City_Id",
                table: "FareCalculations",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareCalculations_Cities_City_Id",
                table: "FareCalculations");

            migrationBuilder.DropIndex(
                name: "IX_FareCalculations_City_Id",
                table: "FareCalculations");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "FareCalculations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FareCalculations_CityId",
                table: "FareCalculations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FareCalculations_Cities_CityId",
                table: "FareCalculations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
