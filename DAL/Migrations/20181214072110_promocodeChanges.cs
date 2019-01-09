using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class promocodeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Promocodes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageCount",
                table: "Promocodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "Details",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "UsageCount",
                table: "Promocodes");
        }
    }
}
