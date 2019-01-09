using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CarModelsUnderCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompanies_Company_Id",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Company_Id",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "CarCompanyId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "CarModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarCompanyId",
                table: "Vehicles",
                column: "CarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_Company_Id",
                table: "CarModels",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_CarCompanies_Company_Id",
                table: "CarModels",
                column: "Company_Id",
                principalTable: "CarCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles",
                column: "CarCompanyId",
                principalTable: "CarCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_CarCompanies_Company_Id",
                table: "CarModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompanies_CarCompanyId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CarCompanyId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_CarModels_Company_Id",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "CarCompanyId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "CarModels");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Company_Id",
                table: "Vehicles",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompanies_Company_Id",
                table: "Vehicles",
                column: "Company_Id",
                principalTable: "CarCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
