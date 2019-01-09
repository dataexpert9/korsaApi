using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AppReferalsUserReferrer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAppReferral_Users_User_Id",
                table: "UserAppReferral");

            migrationBuilder.DropIndex(
                name: "IX_UserAppReferral_User_Id",
                table: "UserAppReferral");

            migrationBuilder.DropColumn(
                name: "AppReferrerUserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "UserAppReferral",
                newName: "InvitedUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAppReferral_InvitedUser_Id",
                table: "UserAppReferral",
                column: "InvitedUser_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppReferral_Users_InvitedUser_Id",
                table: "UserAppReferral",
                column: "InvitedUser_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAppReferral_Users_InvitedUser_Id",
                table: "UserAppReferral");

            migrationBuilder.DropIndex(
                name: "IX_UserAppReferral_InvitedUser_Id",
                table: "UserAppReferral");

            migrationBuilder.RenameColumn(
                name: "InvitedUser_Id",
                table: "UserAppReferral",
                newName: "User_Id");

            migrationBuilder.AddColumn<int>(
                name: "AppReferrerUserId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAppReferral_User_Id",
                table: "UserAppReferral",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppReferral_Users_User_Id",
                table: "UserAppReferral",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
