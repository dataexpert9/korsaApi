using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class paymentHistoryChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PaymentHistories",
                newName: "USDAmount");

            migrationBuilder.AddColumn<double>(
                name: "LocalCurrencyAmount",
                table: "PaymentHistories",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalCurrencyAmount",
                table: "PaymentHistories");

            migrationBuilder.RenameColumn(
                name: "USDAmount",
                table: "PaymentHistories",
                newName: "Amount");
        }
    }
}
