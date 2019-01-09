using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class remainingridesss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingRide",
                table: "CashSubscriptions",
                newName: "RemainingRides");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingRides",
                table: "CashSubscriptions",
                newName: "RemainingRide");
        }
    }
}
