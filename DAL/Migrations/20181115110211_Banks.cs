using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Banks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Branch_Branch_Id",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountML_Account_Account_Id",
                table: "AccountML");

            migrationBuilder.DropForeignKey(
                name: "FK_BankML_Bank_Bank_Id",
                table: "BankML");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Bank_Bank_Id",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchML_Branch_Branch_Id",
                table: "BranchML");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchML",
                table: "BranchML");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankML",
                table: "BankML");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bank",
                table: "Bank");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountML",
                table: "AccountML");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "BranchML",
                newName: "BranchMLs");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branches");

            migrationBuilder.RenameTable(
                name: "BankML",
                newName: "BankMLs");

            migrationBuilder.RenameTable(
                name: "Bank",
                newName: "Banks");

            migrationBuilder.RenameTable(
                name: "AccountML",
                newName: "AccountMLs");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_BranchML_Branch_Id",
                table: "BranchMLs",
                newName: "IX_BranchMLs_Branch_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Branch_Bank_Id",
                table: "Branches",
                newName: "IX_Branches_Bank_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BankML_Bank_Id",
                table: "BankMLs",
                newName: "IX_BankMLs_Bank_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AccountML_Account_Id",
                table: "AccountMLs",
                newName: "IX_AccountMLs_Account_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Branch_Id",
                table: "Accounts",
                newName: "IX_Accounts_Branch_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchMLs",
                table: "BranchMLs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankMLs",
                table: "BankMLs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banks",
                table: "Banks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountMLs",
                table: "AccountMLs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMLs_Accounts_Account_Id",
                table: "AccountMLs",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Branches_Branch_Id",
                table: "Accounts",
                column: "Branch_Id",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankMLs_Banks_Bank_Id",
                table: "BankMLs",
                column: "Bank_Id",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Banks_Bank_Id",
                table: "Branches",
                column: "Bank_Id",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchMLs_Branches_Branch_Id",
                table: "BranchMLs",
                column: "Branch_Id",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMLs_Accounts_Account_Id",
                table: "AccountMLs");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Branches_Branch_Id",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankMLs_Banks_Bank_Id",
                table: "BankMLs");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Banks_Bank_Id",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchMLs_Branches_Branch_Id",
                table: "BranchMLs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchMLs",
                table: "BranchMLs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banks",
                table: "Banks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankMLs",
                table: "BankMLs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountMLs",
                table: "AccountMLs");

            migrationBuilder.RenameTable(
                name: "BranchMLs",
                newName: "BranchML");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branch");

            migrationBuilder.RenameTable(
                name: "Banks",
                newName: "Bank");

            migrationBuilder.RenameTable(
                name: "BankMLs",
                newName: "BankML");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameTable(
                name: "AccountMLs",
                newName: "AccountML");

            migrationBuilder.RenameIndex(
                name: "IX_BranchMLs_Branch_Id",
                table: "BranchML",
                newName: "IX_BranchML_Branch_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_Bank_Id",
                table: "Branch",
                newName: "IX_Branch_Bank_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BankMLs_Bank_Id",
                table: "BankML",
                newName: "IX_BankML_Bank_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Branch_Id",
                table: "Account",
                newName: "IX_Account_Branch_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AccountMLs_Account_Id",
                table: "AccountML",
                newName: "IX_AccountML_Account_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchML",
                table: "BranchML",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bank",
                table: "Bank",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankML",
                table: "BankML",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountML",
                table: "AccountML",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Branch_Branch_Id",
                table: "Account",
                column: "Branch_Id",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountML_Account_Account_Id",
                table: "AccountML",
                column: "Account_Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankML_Bank_Bank_Id",
                table: "BankML",
                column: "Bank_Id",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Bank_Bank_Id",
                table: "Branch",
                column: "Bank_Id",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchML_Branch_Branch_Id",
                table: "BranchML",
                column: "Branch_Id",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
