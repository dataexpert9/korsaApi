using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class MessageMLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "CancellationReason_Id",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Culture",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CancellationReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageMLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Message_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageMLs_Messages_Message_Id",
                        column: x => x.Message_Id,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancellationReasonMLs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CancellationReasonId = table.Column<int>(type: "int", nullable: true),
                    CancellationReason_Id = table.Column<int>(type: "int", nullable: false),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasonMLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancellationReasonMLs_CancellationReasons_CancellationReasonId",
                        column: x => x.CancellationReasonId,
                        principalTable: "CancellationReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancellationReasonMLs_RideTypes_CancellationReason_Id",
                        column: x => x.CancellationReason_Id,
                        principalTable: "RideTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CancellationReason_Id",
                table: "Trips",
                column: "CancellationReason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReasonId",
                table: "CancellationReasonMLs",
                column: "CancellationReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationReasonMLs_CancellationReason_Id",
                table: "CancellationReasonMLs",
                column: "CancellationReason_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMLs_Message_Id",
                table: "MessageMLs",
                column: "Message_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_CancellationReasons_CancellationReason_Id",
                table: "Trips",
                column: "CancellationReason_Id",
                principalTable: "CancellationReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_CancellationReasons_CancellationReason_Id",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "CancellationReasonMLs");

            migrationBuilder.DropTable(
                name: "MessageMLs");

            migrationBuilder.DropTable(
                name: "CancellationReasons");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CancellationReason_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CancellationReason_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Messages",
                nullable: true);
        }
    }
}
