using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SubscriptionPackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPackages",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NumOfRides = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    DurationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverSubscriptions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Driver_Id = table.Column<int>(nullable: false),
                    SubscriptionPackage_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverSubscriptions_Drivers_Driver_Id",
                        column: x => x.Driver_Id,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverSubscriptions_SubscriptionPackages_SubscriptionPackage_Id",
                        column: x => x.SubscriptionPackage_Id,
                        principalTable: "SubscriptionPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverSubscriptions_Driver_Id",
                table: "DriverSubscriptions",
                column: "Driver_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSubscriptions_SubscriptionPackage_Id",
                table: "DriverSubscriptions",
                column: "SubscriptionPackage_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPackages");
        }
    }
}
