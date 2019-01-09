using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class publicdoubleWalletDeductiongetset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WalletDeduction",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletDeduction",
                table: "Trips");
        }
    }
}
