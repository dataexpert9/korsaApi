using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Wallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentReferralCount",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeRides",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Wallet",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RideTax",
                table: "Settings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Wallet",
                table: "Drivers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentReferralCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FreeRides",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RideTax",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Drivers");
        }
    }
}
