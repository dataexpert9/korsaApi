using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class cardID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveCardNumber",
                table: "Users",
                newName: "ActiveCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveCardId",
                table: "Users",
                newName: "ActiveCardNumber");
        }
    }
}
