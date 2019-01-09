using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Cashsubcription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashSubscriptions_Accounts_AccountId",
                table: "CashSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CashSubscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "CashSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CashSubscriptions_AccountId",
                table: "CashSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CashSubscriptions_SubscriptionPackageId",
                table: "CashSubscriptions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "CashSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPackageId",
                table: "CashSubscriptions");

            migrationBuilder.AddColumn<double>(
                name: "CollectCash",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "CashSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_Account_Id",
                table: "CashSubscriptions",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_SubscriptionPackage_Id",
                table: "CashSubscriptions",
                column: "SubscriptionPackage_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashSubscriptions_Accounts_Account_Id",
                table: "CashSubscriptions",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashSubscriptions_SubscriptionPackages_SubscriptionPackage_Id",
                table: "CashSubscriptions",
                column: "SubscriptionPackage_Id",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashSubscriptions_Accounts_Account_Id",
                table: "CashSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CashSubscriptions_SubscriptionPackages_SubscriptionPackage_Id",
                table: "CashSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CashSubscriptions_Account_Id",
                table: "CashSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CashSubscriptions_SubscriptionPackage_Id",
                table: "CashSubscriptions");

            migrationBuilder.DropColumn(
                name: "CollectCash",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "CashSubscriptions");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "CashSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPackageId",
                table: "CashSubscriptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_AccountId",
                table: "CashSubscriptions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashSubscriptions_SubscriptionPackageId",
                table: "CashSubscriptions",
                column: "SubscriptionPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashSubscriptions_Accounts_AccountId",
                table: "CashSubscriptions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashSubscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "CashSubscriptions",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
