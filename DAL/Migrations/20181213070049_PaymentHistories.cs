using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PaymentHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_Drivers_Driver_Id",
                table: "PaymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_Users_User_Id",
                table: "PaymentHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentHistory",
                table: "PaymentHistory");

            migrationBuilder.RenameTable(
                name: "PaymentHistory",
                newName: "PaymentHistories");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistory_User_Id",
                table: "PaymentHistories",
                newName: "IX_PaymentHistories_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistory_Driver_Id",
                table: "PaymentHistories",
                newName: "IX_PaymentHistories_Driver_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentHistories",
                table: "PaymentHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistories_Drivers_Driver_Id",
                table: "PaymentHistories",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistories_Users_User_Id",
                table: "PaymentHistories",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistories_Drivers_Driver_Id",
                table: "PaymentHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistories_Users_User_Id",
                table: "PaymentHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentHistories",
                table: "PaymentHistories");

            migrationBuilder.RenameTable(
                name: "PaymentHistories",
                newName: "PaymentHistory");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistories_User_Id",
                table: "PaymentHistory",
                newName: "IX_PaymentHistory_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentHistories_Driver_Id",
                table: "PaymentHistory",
                newName: "IX_PaymentHistory_Driver_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentHistory",
                table: "PaymentHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_Drivers_Driver_Id",
                table: "PaymentHistory",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_Users_User_Id",
                table: "PaymentHistory",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
