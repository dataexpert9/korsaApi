using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UniqueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Drivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Drivers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Drivers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
