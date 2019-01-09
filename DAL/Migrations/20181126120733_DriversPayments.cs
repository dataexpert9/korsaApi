using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DriversPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverPayments_Drivers_DriverId",
                table: "DriverPayments");

            migrationBuilder.DropIndex(
                name: "IX_DriverPayments_DriverId",
                table: "DriverPayments");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DriverPayments");

            migrationBuilder.CreateIndex(
                name: "IX_DriverPayments_Driver_Id",
                table: "DriverPayments",
                column: "Driver_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverPayments_Drivers_Driver_Id",
                table: "DriverPayments",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverPayments_Drivers_Driver_Id",
                table: "DriverPayments");

            migrationBuilder.DropIndex(
                name: "IX_DriverPayments_Driver_Id",
                table: "DriverPayments");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "DriverPayments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverPayments_DriverId",
                table: "DriverPayments",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverPayments_Drivers_DriverId",
                table: "DriverPayments",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
