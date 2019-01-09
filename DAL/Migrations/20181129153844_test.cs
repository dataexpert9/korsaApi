using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test : Migration
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
                  Price = table.Column<double>(nullable: false),
                  Duration = table.Column<int>(nullable: false),
                  DurationType = table.Column<int>(nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_SubscriptionPackages", x => x.Id);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
    name: "SubscriptionPackages");
        }
    }
}
