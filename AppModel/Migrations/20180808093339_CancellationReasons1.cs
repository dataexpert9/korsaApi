using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class CancellationReasons1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReasonId",
                table: "CancellationReasonMLs");

            migrationBuilder.DropIndex(
                name: "IX_CancellationReasonMLs_CancellationReasonId",
                table: "CancellationReasonMLs");

            migrationBuilder.DropColumn(
                name: "CancellationReasonId",
                table: "CancellationReasonMLs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CancellationReasonId",
                table: "CancellationReasonMLs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReasonId",
                table: "CancellationReasonMLs",
                column: "CancellationReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReasonId",
                table: "CancellationReasonMLs",
                column: "CancellationReasonId",
                principalTable: "CancellationReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
