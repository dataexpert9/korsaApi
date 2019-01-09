using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PromocodeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isValid",
                table: "Promocodes");

            migrationBuilder.AddColumn<string>(
                name: "AboutRideType",
                table: "RideTypeMLs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Promocodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutRideType",
                table: "RideTypeMLs");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Promocodes");

            migrationBuilder.AddColumn<bool>(
                name: "isValid",
                table: "Promocodes",
                nullable: false,
                defaultValue: false);
        }
    }
}
