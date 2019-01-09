using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class waste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppReferrerUserId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppReferrerUserId",
                table: "Users");
        }
    }
}
