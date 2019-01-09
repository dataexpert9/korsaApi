using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AdminNotifications_AdminNotificationId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Drivers_DriverId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AdminNotificationId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DriverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AdminNotificationId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeliveryMan_ID",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Text_Ar",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Title_Ar",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "User_ID",
                table: "Notifications",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "Entity_ID",
                table: "Notifications",
                newName: "Entity_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "Driver_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_Driver_Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(nullable: true),
                    Admin_Id = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminTokens_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdminNotification_Id",
                table: "Notifications",
                column: "AdminNotification_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_User_Id",
                table: "Notifications",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AdminTokens_AdminId",
                table: "AdminTokens",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AdminNotifications_AdminNotification_Id",
                table: "Notifications",
                column: "AdminNotification_Id",
                principalTable: "AdminNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Drivers_Driver_Id",
                table: "Notifications",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_User_Id",
                table: "Notifications",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AdminNotifications_AdminNotification_Id",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Drivers_Driver_Id",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_User_Id",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "AdminTokens");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AdminNotification_Id",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_User_Id",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Notifications",
                newName: "User_ID");

            migrationBuilder.RenameColumn(
                name: "Entity_Id",
                table: "Notifications",
                newName: "Entity_ID");

            migrationBuilder.RenameColumn(
                name: "Driver_Id",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_Driver_Id",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AddColumn<int>(
                name: "AdminNotificationId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMan_ID",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text_Ar",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_Ar",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AdminNotificationId",
                table: "Notifications",
                column: "AdminNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DriverId",
                table: "Notifications",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AdminNotifications_AdminNotificationId",
                table: "Notifications",
                column: "AdminNotificationId",
                principalTable: "AdminNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Drivers_DriverId",
                table: "Notifications",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
