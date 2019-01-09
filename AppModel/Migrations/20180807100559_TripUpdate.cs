using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class TripUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "Promocode_Id",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Driver_Id",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips",
                column: "Promocode_Id",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "Promocode_Id",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Driver_Id",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips",
                column: "Promocode_Id",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
