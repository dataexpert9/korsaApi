using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AppReferals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
              name: "InvitedFriends",
              columns: table => new
              {
                  CreatedDate = table.Column<DateTime>(nullable: false),
                  ModifiedDate = table.Column<DateTime>(nullable: true),
                  IsDeleted = table.Column<bool>(nullable: false),
                  Id = table.Column<int>(nullable: false)
                      .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  InvitedUser_Id = table.Column<int>(nullable: false),
                  Referrer_Id = table.Column<int>(nullable: false),
                  ReferrerId = table.Column<int>(nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_InvitedFriends", x => x.Id);
                  table.ForeignKey(
                      name: "FK_InvitedFriends_Users_InvitedUser_Id",
                      column: x => x.InvitedUser_Id,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_InvitedFriends_Users_ReferrerId",
                      column: x => x.ReferrerId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
              });






            //migrationBuilder.CreateTable(
            //    name: "UserAppReferral",
            //    columns: table => new
            //    {
            //        CreatedDate = table.Column<DateTime>(nullable: false),
            //        ModifiedDate = table.Column<DateTime>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        User_Id = table.Column<int>(nullable: false),
            //        Referrer_Id = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserAppReferral", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserAppReferral_Users_Referrer_Id",
            //            column: x => x.Referrer_Id,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserAppReferral_Users_User_Id",
            //            column: x => x.User_Id,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAppReferral_Referrer_Id",
            //    table: "UserAppReferral",
            //    column: "Referrer_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAppReferral_User_Id",
            //    table: "UserAppReferral",
            //    column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitedFriends");

        }
    }
}
