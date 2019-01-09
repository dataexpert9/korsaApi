using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class KORSADistanceTravelled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DistanceTravelled",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceTravelled",
                table: "Users");
        }
    }
}
