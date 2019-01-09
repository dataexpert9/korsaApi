using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AlphaSevicesBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserDevices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserDevices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UserDevices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SettingsMLs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SettingsMLs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "SettingsMLs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Settings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RideTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RideTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "RideTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RideTypeMLs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RideTypeMLs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "RideTypeMLs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AppRatingMLs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppRatingMLs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AppRatingMLs",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SettingsMLs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SettingsMLs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "SettingsMLs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RideTypeMLs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RideTypeMLs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "RideTypeMLs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AppRatingMLs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppRatingMLs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AppRatingMLs");
        }
    }
}
