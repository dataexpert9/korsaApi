using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AlphaUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsML");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppRatings");

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "RideTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "AppRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppRatingMLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppRating_Id = table.Column<int>(type: "int", nullable: false),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRatingMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRatingMLs_AppRatings_AppRating_Id",
                        column: x => x.AppRating_Id,
                        principalTable: "AppRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideTypeMLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RideType_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideTypeMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideTypeMLs_RideTypes_RideType_Id",
                        column: x => x.RideType_Id,
                        principalTable: "RideTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingsMLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivacyPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settings_Id = table.Column<int>(type: "int", nullable: false),
                    TermsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsMLs_Settings_Settings_Id",
                        column: x => x.Settings_Id,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRatingMLs_AppRating_Id",
                table: "AppRatingMLs",
                column: "AppRating_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RideTypeMLs_RideType_Id",
                table: "RideTypeMLs",
                column: "RideType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsMLs_Settings_Id",
                table: "SettingsMLs",
                column: "Settings_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRatingMLs");

            migrationBuilder.DropTable(
                name: "RideTypeMLs");

            migrationBuilder.DropTable(
                name: "SettingsMLs");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "RideTypes");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "AppRatings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppRatings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SettingsML",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutUs = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    Settings_Id = table.Column<int>(nullable: false),
                    TermsOfUse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsML", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsML_Settings_Settings_Id",
                        column: x => x.Settings_Id,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettingsML_Settings_Id",
                table: "SettingsML",
                column: "Settings_Id");
        }
    }
}
