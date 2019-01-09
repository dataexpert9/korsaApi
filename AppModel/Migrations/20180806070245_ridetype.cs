using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class ridetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "VerifyNumberCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultImageUrl",
                table: "RideTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PeakFactor",
                table: "RideTypes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "SelectedImageUrl",
                table: "RideTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "VerifyNumberCodes");

            migrationBuilder.DropColumn(
                name: "DefaultImageUrl",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "PeakFactor",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "SelectedImageUrl",
                table: "RideTypes");
        }
    }
}
