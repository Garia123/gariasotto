using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTravel.DataAccess.Migrations
{
    public partial class ImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Reviews_ReviewId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_ReviewId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Reserves");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReserveId",
                table: "Reviews",
                column: "ReserveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reserves_ReserveId",
                table: "Reviews",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reserves_ReserveId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReserveId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Reserves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_ReviewId",
                table: "Reserves",
                column: "ReviewId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Reviews_ReviewId",
                table: "Reserves",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
