using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FareCalculationRideTypebased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RideType_Id",
                table: "FareCalculations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FareCalculations_RideType_Id",
                table: "FareCalculations",
                column: "RideType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FareCalculations_RideTypes_RideType_Id",
                table: "FareCalculations",
                column: "RideType_Id",
                principalTable: "RideTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FareCalculations_RideTypes_RideType_Id",
                table: "FareCalculations");

            migrationBuilder.DropIndex(
                name: "IX_FareCalculations_RideType_Id",
                table: "FareCalculations");

            migrationBuilder.DropColumn(
                name: "RideType_Id",
                table: "FareCalculations");
        }
    }
}
