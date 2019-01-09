using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class CancellationReasons12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CancellationReason_Id",
                table: "CancellationReasonMLs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReason_Id",
                table: "CancellationReasonMLs",
                column: "CancellationReason_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReason_Id",
                table: "CancellationReasonMLs",
                column: "CancellationReason_Id",
                principalTable: "CancellationReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReason_Id",
                table: "CancellationReasonMLs");

            migrationBuilder.DropIndex(
                name: "IX_CancellationReasonMLs_CancellationReason_Id",
                table: "CancellationReasonMLs");

            migrationBuilder.DropColumn(
                name: "CancellationReason_Id",
                table: "CancellationReasonMLs");
        }
    }
}
