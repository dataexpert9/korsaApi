using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class IBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AccountMLs",
                newName: "IBN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IBN",
                table: "AccountMLs",
                newName: "Address");
        }
    }
}
