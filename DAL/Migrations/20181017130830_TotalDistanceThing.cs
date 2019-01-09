using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class TotalDistanceThing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalMileage",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalMileage",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
