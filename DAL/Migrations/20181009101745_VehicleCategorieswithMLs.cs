using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class VehicleCategorieswithMLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompany_Company_Id",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarModel_Model_Id",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarType_Type_Id",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarType",
                table: "CarType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCompany",
                table: "CarCompany");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarYear");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarCapacity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarCompany");

            migrationBuilder.RenameTable(
                name: "CarType",
                newName: "CarTypes");

            migrationBuilder.RenameTable(
                name: "CarModel",
                newName: "CarModels");

            migrationBuilder.RenameTable(
                name: "CarCompany",
                newName: "CarCompanies");

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "CarYear",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "CarCapacity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "CarTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "CarModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "CarCompanies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarTypes",
                table: "CarTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCompanies",
                table: "CarCompanies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarCapacityMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarCapacity_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCapacityMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarCapacityMLs_CarCapacity_CarCapacity_Id",
                        column: x => x.CarCapacity_Id,
                        principalTable: "CarCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanyMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarCompany_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanyMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarCompanyMLs_CarCompanies_CarCompany_Id",
                        column: x => x.CarCompany_Id,
                        principalTable: "CarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarModelMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarModel_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModelMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModelMLs_CarModels_CarModel_Id",
                        column: x => x.CarModel_Id,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTypeMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarType_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypeMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarTypeMLs_CarTypes_CarType_Id",
                        column: x => x.CarType_Id,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarYearMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Culture = table.Column<int>(nullable: false),
                    CarYear_Id = table.Column<int>(nullable: false),
                    CarCapacityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarYearMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarYearMLs_CarCapacity_CarCapacityId",
                        column: x => x.CarCapacityId,
                        principalTable: "CarCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarYearMLs_CarYear_CarYear_Id",
                        column: x => x.CarYear_Id,
                        principalTable: "CarYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarCapacityMLs_CarCapacity_Id",
                table: "CarCapacityMLs",
                column: "CarCapacity_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarCompanyMLs_CarCompany_Id",
                table: "CarCompanyMLs",
                column: "CarCompany_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarModelMLs_CarModel_Id",
                table: "CarModelMLs",
                column: "CarModel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypeMLs_CarType_Id",
                table: "CarTypeMLs",
                column: "CarType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarYearMLs_CarCapacityId",
                table: "CarYearMLs",
                column: "CarCapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarYearMLs_CarYear_Id",
                table: "CarYearMLs",
                column: "CarYear_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompanies_Company_Id",
                table: "Vehicles",
                column: "Company_Id",
                principalTable: "CarCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarModels_Model_Id",
                table: "Vehicles",
                column: "Model_Id",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarTypes_Type_Id",
                table: "Vehicles",
                column: "Type_Id",
                principalTable: "CarTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCompanies_Company_Id",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarModels_Model_Id",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarTypes_Type_Id",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "CarCapacityMLs");

            migrationBuilder.DropTable(
                name: "CarCompanyMLs");

            migrationBuilder.DropTable(
                name: "CarModelMLs");

            migrationBuilder.DropTable(
                name: "CarTypeMLs");

            migrationBuilder.DropTable(
                name: "CarYearMLs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarTypes",
                table: "CarTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCompanies",
                table: "CarCompanies");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "CarYear");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "CarCapacity");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "CarTypes");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "CarCompanies");

            migrationBuilder.RenameTable(
                name: "CarTypes",
                newName: "CarType");

            migrationBuilder.RenameTable(
                name: "CarModels",
                newName: "CarModel");

            migrationBuilder.RenameTable(
                name: "CarCompanies",
                newName: "CarCompany");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarYear",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarCapacity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarCompany",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarType",
                table: "CarType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCompany",
                table: "CarCompany",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCompany_Company_Id",
                table: "Vehicles",
                column: "Company_Id",
                principalTable: "CarCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarModel_Model_Id",
                table: "Vehicles",
                column: "Model_Id",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarType_Type_Id",
                table: "Vehicles",
                column: "Type_Id",
                principalTable: "CarType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
