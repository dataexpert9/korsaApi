using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class promocodeNewParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationDate",
                table: "Promocodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LimitOfUsage",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Drivers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationDate",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "LimitOfUsage",
                table: "Promocodes");

            migrationBuilder.AlterColumn<int>(
                name: "City_Id",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
