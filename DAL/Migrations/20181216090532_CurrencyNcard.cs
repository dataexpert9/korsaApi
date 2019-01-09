using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CurrencyNcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveCardNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrencyToUSDRatio",
                table: "Settings",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCardNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrencyToUSDRatio",
                table: "Settings");
        }
    }
}
