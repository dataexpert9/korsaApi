using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AccountOptionalNpaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Account_Id",
                table: "CashSubscriptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "CashSubscriptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "CashSubscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "Account_Id",
                table: "CashSubscriptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
