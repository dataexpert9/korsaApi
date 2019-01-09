using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DriverContactUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "ContactUs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Driver_Id",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_Driver_Id",
                table: "ContactUs",
                column: "Driver_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Drivers_Driver_Id",
                table: "ContactUs",
                column: "Driver_Id",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Drivers_Driver_Id",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_Driver_Id",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Driver_Id",
                table: "ContactUs");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "ContactUs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
