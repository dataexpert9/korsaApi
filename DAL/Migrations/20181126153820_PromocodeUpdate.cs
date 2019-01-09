using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PromocodeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_Users_UserId",
                table: "Promocodes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Promocodes",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Promocodes_UserId",
                table: "Promocodes",
                newName: "IX_Promocodes_User_Id");

            migrationBuilder.AddColumn<int>(
                name: "CodeType",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CouponAmount",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CouponType",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Promocodes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpiryHours",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Promocodes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Promocodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Promocodes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_Users_User_Id",
                table: "Promocodes",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_Users_User_Id",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "CodeType",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "CouponAmount",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "CouponType",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "ExpiryHours",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Promocodes");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Promocodes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Promocodes_User_Id",
                table: "Promocodes",
                newName: "IX_Promocodes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_Users_UserId",
                table: "Promocodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
