using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class cancelReasonTextInRideCancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarYearMLs_CarCapacity_CarCapacityId",
                table: "CarYearMLs");

            migrationBuilder.DropIndex(
                name: "IX_CarYearMLs_CarCapacityId",
                table: "CarYearMLs");

            migrationBuilder.DropColumn(
                name: "CarCapacityId",
                table: "CarYearMLs");

            migrationBuilder.AddColumn<string>(
                name: "ReasonToCancel",
                table: "Trips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonToCancel",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "CarCapacityId",
                table: "CarYearMLs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarYearMLs_CarCapacityId",
                table: "CarYearMLs",
                column: "CarCapacityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarYearMLs_CarCapacity_CarCapacityId",
                table: "CarYearMLs",
                column: "CarCapacityId",
                principalTable: "CarCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
