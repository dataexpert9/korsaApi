using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class remainingRide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingRide",
                table: "CashSubscriptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingRide",
                table: "CashSubscriptions");
        }
    }
}
