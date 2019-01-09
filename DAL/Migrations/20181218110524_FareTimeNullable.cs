using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FareTimeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<double>(
            //    name: "WalletDeduction",
            //    table: "Trips",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "FareCalculations",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "FareCalculations",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "WalletDeduction",
            //    table: "Trips");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "FareCalculations",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "FareCalculations",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
