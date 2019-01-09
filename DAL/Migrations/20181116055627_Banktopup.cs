using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Banktopup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankTopUps",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Account_Id = table.Column<int>(nullable: false),
                    //Branch_Id = table.Column<int>(nullable: false),
                    //Bank_Id = table.Column<int>(nullable: false),
                    //BankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTopUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTopUps_Accounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_BankTopUps_Banks_BankId",
                    //    column: x => x.BankId,
                    //    principalTable: "Banks",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Restrict);
                    //table.ForeignKey(
                    //    name: "FK_BankTopUps_Branches_Branch_Id",
                    //    column: x => x.Branch_Id,
                    //    principalTable: "Branches",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTopUps_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankTopUps_Account_Id",
                table: "BankTopUps",
                column: "Account_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BankTopUps_BankId",
            //    table: "BankTopUps",
            //    column: "BankId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BankTopUps_Branch_Id",
            //    table: "BankTopUps",
            //    column: "Branch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankTopUps_User_Id",
                table: "BankTopUps",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTopUps");
        }
    }
}
