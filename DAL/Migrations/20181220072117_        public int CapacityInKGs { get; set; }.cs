using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class publicintCapacityInKGsgetset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacityInKGs",
                table: "RideTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacityInKGs",
                table: "RideTypes");
        }
    }
}
