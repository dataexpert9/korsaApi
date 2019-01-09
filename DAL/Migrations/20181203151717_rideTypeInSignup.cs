using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class rideTypeInSignup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RideType_Id",
                table: "Vehicles",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RideType_Id",
                table: "Vehicles",
                column: "RideType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_RideTypes_RideType_Id",
                table: "Vehicles",
                column: "RideType_Id",
                principalTable: "RideTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_RideTypes_RideType_Id",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_RideType_Id",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "RideType_Id",
                table: "Vehicles");
        }
    }
}
