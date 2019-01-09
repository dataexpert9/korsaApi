using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class isVerifiedRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "VerifyNumberCodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "VerifyNumberCodes",
                nullable: false,
                defaultValue: false);
        }
    }
}
