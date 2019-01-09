using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class TryChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitedFriends_Users_ReferrerId",
                table: "InvitedFriends");

            //migrationBuilder.DropIndex(
            //    name: "IX_InvitedFriends_ReferrerId",
            //    table: "InvitedFriends");

            migrationBuilder.DropColumn(
                name: "ReferrerId",
                table: "InvitedFriends");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedFriends_Referrer_Id",
                table: "InvitedFriends",
                column: "Referrer_Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_InvitedFriends_Users_Referrer_Id",
            //    table: "InvitedFriends",
            //    column: "Referrer_Id",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_InvitedFriends_Users_Referrer_Id",
            //    table: "InvitedFriends");

            migrationBuilder.DropIndex(
                name: "IX_InvitedFriends_Referrer_Id",
                table: "InvitedFriends");

            migrationBuilder.AddColumn<int>(
                name: "ReferrerId",
                table: "InvitedFriends",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_InvitedFriends_ReferrerId",
            //    table: "InvitedFriends",
            //    column: "ReferrerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitedFriends_Users_ReferrerId",
                table: "InvitedFriends",
                column: "ReferrerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
