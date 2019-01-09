using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class TripUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Trips_TripId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TripId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryUser_Id",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips",
                column: "PrimaryUser_Id",
                unique: true,
                filter: "[PrimaryUser_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_PrimaryUser_Id",
                table: "Trips",
                column: "PrimaryUser_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TripId",
                table: "Users",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Trips_TripId",
                table: "Users",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
