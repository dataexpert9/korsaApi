using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class TripUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_PrimaryUserId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PrimaryUserId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PrimaryUserId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Users",
                type: "int",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PrimaryUserId",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PrimaryUserId",
                table: "Trips",
                column: "PrimaryUserId",
                unique: true,
                filter: "[PrimaryUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_PrimaryUserId",
                table: "Trips",
                column: "PrimaryUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
