using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class testPrimaryUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips",
                column: "PrimaryUser_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_PrimaryUser_Id",
                table: "Trips",
                column: "PrimaryUser_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_PrimaryUser_Id",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PrimaryUser_Id",
                table: "Trips");

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
    }
}
