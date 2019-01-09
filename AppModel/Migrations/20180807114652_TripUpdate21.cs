using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class TripUpdate21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPromocode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Promocode_Id = table.Column<int>(type: "int", nullable: false),
                    UsageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromocode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPromocode_Promocodes_Promocode_Id",
                        column: x => x.Promocode_Id,
                        principalTable: "Promocodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPromocode_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocode_Promocode_Id",
                table: "UserPromocode",
                column: "Promocode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPromocode_User_Id",
                table: "UserPromocode",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPromocode");
        }
    }
}
