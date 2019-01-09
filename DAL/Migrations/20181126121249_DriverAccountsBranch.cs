using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DriverAccountsBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAccounts_Branches_BranchId",
                table: "DriverAccounts");

            migrationBuilder.DropIndex(
                name: "IX_DriverAccounts_BranchId",
                table: "DriverAccounts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "DriverAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAccounts_Branch_Id",
                table: "DriverAccounts",
                column: "Branch_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAccounts_Branches_Branch_Id",
                table: "DriverAccounts",
                column: "Branch_Id",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAccounts_Branches_Branch_Id",
                table: "DriverAccounts");

            migrationBuilder.DropIndex(
                name: "IX_DriverAccounts_Branch_Id",
                table: "DriverAccounts");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "DriverAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverAccounts_BranchId",
                table: "DriverAccounts",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAccounts_Branches_BranchId",
                table: "DriverAccounts",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
