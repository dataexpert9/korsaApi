using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RolesOnScreens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "Admins",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Name_Ar = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Screen_Id = table.Column<int>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false),
                    AllowScreen = table.Column<bool>(nullable: false),
                    Fullaccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleScreens_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreens_Screens_Screen_Id",
                        column: x => x.Screen_Id,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Role_Id",
                table: "Admins",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreens_Role_Id",
                table: "RoleScreens",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreens_Screen_Id",
                table: "RoleScreens",
                column: "Screen_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Roles_Role_Id",
                table: "Admins",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Roles_Role_Id",
                table: "Admins");

            migrationBuilder.DropTable(
                name: "RoleScreens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropIndex(
                name: "IX_Admins_Role_Id",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "Admins");
        }
    }
}
