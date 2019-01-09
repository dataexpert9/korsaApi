using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FareCalculationParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BasicCharges",
                table: "FareCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FarePerKM",
                table: "FareCalculations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FarePerMin",
                table: "FareCalculations",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicCharges",
                table: "FareCalculations");

            migrationBuilder.DropColumn(
                name: "FarePerKM",
                table: "FareCalculations");

            migrationBuilder.DropColumn(
                name: "FarePerMin",
                table: "FareCalculations");
        }
    }
}
