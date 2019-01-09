using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Account_Id",
                table: "BankTopUps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "BankTopUps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "BankTopUps");

            migrationBuilder.AlterColumn<int>(
                name: "Account_Id",
                table: "BankTopUps",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
