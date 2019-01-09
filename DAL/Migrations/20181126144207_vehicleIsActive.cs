using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class vehicleIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Vehicles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UseCreditFirst",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UseCreditFirst",
                table: "Users");
        }
    }
}
