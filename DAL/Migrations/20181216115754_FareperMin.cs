using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FareperMin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FarePerKm",
                table: "RideTypes",
                newName: "FarePerKM");

            migrationBuilder.AddColumn<double>(
                name: "FarePerMin",
                table: "RideTypes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarePerMin",
                table: "RideTypes");

            migrationBuilder.RenameColumn(
                name: "FarePerKM",
                table: "RideTypes",
                newName: "FarePerKm");
        }
    }
}
