using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class wastage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppReferrerUserId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "AppReferrerUserId",
            //    table: "Users",
            //    nullable: false,
            //    defaultValue: 0);
            migrationBuilder.DropColumn(
          name: "AppReferrerUserId",
          table: "Users");
        }
    }
}
