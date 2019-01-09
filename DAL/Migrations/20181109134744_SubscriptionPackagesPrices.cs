using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SubscriptionPackagesPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SubscriptionPackages",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "DriverSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RemainingRides",
                table: "DriverSubscriptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "SubscriptionPackages");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "DriverSubscriptions");

            migrationBuilder.DropColumn(
                name: "RemainingRides",
                table: "DriverSubscriptions");
        }
    }
}
