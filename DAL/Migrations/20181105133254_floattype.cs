using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class floattype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UserRating",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<double>(
                name: "DriverRating",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "UserRating",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<short>(
                name: "DriverRating",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
