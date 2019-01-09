using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RatingFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeedbackForDriver",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedbackForUser",
                table: "Trips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackForDriver",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FeedbackForUser",
                table: "Trips");
        }
    }
}
