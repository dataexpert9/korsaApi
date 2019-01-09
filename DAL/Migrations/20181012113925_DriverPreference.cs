using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DriverPreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminNotifications_Admin_AdminId",
                table: "AdminNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminSubAdminNotifications_Admin_AdminId",
                table: "AdminSubAdminNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Admins");

            migrationBuilder.AddColumn<int>(
                name: "DriverPreference",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminNotifications_Admins_AdminId",
                table: "AdminNotifications",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminSubAdminNotifications_Admins_AdminId",
                table: "AdminSubAdminNotifications",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminNotifications_Admins_AdminId",
                table: "AdminNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminSubAdminNotifications_Admins_AdminId",
                table: "AdminSubAdminNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "DriverPreference",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminNotifications_Admin_AdminId",
                table: "AdminNotifications",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminSubAdminNotifications_Admin_AdminId",
                table: "AdminSubAdminNotifications",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
