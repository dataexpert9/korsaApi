using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CreditCardInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "UserCreditCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditCards_User_Id",
                table: "UserCreditCards",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreditCards_Users_User_Id",
                table: "UserCreditCards",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCreditCards_Users_User_Id",
                table: "UserCreditCards");

            migrationBuilder.DropIndex(
                name: "IX_UserCreditCards_User_Id",
                table: "UserCreditCards");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "UserCreditCards");
        }
    }
}
