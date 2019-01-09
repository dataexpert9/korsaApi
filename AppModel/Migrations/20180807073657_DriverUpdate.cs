using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class DriverUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promocode",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "Promocode_Id",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarColor",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarName",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarNumber",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Drivers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    isValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promocodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDriverMessagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Driver_Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Message_Id = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDriverMessagings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDriverMessagings_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDriverMessagings_Messages_Message_Id",
                        column: x => x.Message_Id,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDriverMessagings_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Promocode_Id",
                table: "Trips",
                column: "Promocode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_UserId",
                table: "Promocodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverMessagings_Driver_Id",
                table: "UserDriverMessagings",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverMessagings_Message_Id",
                table: "UserDriverMessagings",
                column: "Message_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverMessagings_User_Id",
                table: "UserDriverMessagings",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips",
                column: "Promocode_Id",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Promocodes_Promocode_Id",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropTable(
                name: "UserDriverMessagings");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Promocode_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Promocode_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CarColor",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CarName",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CarNumber",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "Promocode",
                table: "Trips",
                nullable: true);
        }
    }
}
