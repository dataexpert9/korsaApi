using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Cities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Sates_State_Id",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Sates");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_State_Id",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "PhoneCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "SortName",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "State_Id",
                table: "Cities");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Countries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Countries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Cities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Cities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CityMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    City_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityMLs_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryMLs",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Country_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryMLs_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country_Id",
                table: "Cities",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CityMLs_City_Id",
                table: "CityMLs",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMLs_Country_Id",
                table: "CountryMLs",
                column: "Country_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_Country_Id",
                table: "Cities",
                column: "Country_Id",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_Country_Id",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "CityMLs");

            migrationBuilder.DropTable(
                name: "CountryMLs");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Country_Id",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneCode",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SortName",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State_Id",
                table: "Cities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sates_Countries_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_State_Id",
                table: "Cities",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sates_Country_Id",
                table: "Sates",
                column: "Country_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Sates_State_Id",
                table: "Cities",
                column: "State_Id",
                principalTable: "Sates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
