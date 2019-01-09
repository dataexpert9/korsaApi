using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class cashsubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashSubscriptions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Driver_Id = table.Column<int>(nullable: false),
                    Account_Id = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    SubscriptionPackage_Id = table.Column<int>(nullable: false),
                    SubscriptionPackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashSubscriptions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashSubscriptions_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashSubscriptions_SubscriptionPackages_SubscriptionPackageId",
                        column: x => x.SubscriptionPackageId,
                        principalTable: "SubscriptionPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashSubscriptionMedias",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: true),
                    CashSubscription_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashSubscriptionMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashSubscriptionMedias_CashSubscriptions_CashSubscription_Id",
                        column: x => x.CashSubscription_Id,
                        principalTable: "CashSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptionMedias_CashSubscription_Id",
                table: "CashSubscriptionMedias",
                column: "CashSubscription_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_AccountId",
                table: "CashSubscriptions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_Driver_Id",
                table: "CashSubscriptions",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_SubscriptionPackageId",
                table: "CashSubscriptions",
                column: "SubscriptionPackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashSubscriptionMedias");

            migrationBuilder.DropTable(
                name: "CashSubscriptions");
        }
    }
}
