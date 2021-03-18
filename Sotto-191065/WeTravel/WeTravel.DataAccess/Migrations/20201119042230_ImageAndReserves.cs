using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTravel.DataAccess.Migrations
{
    public partial class ImageAndReserves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TouristLocations");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Lodgings");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "TouristLocations",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Reserves",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    LodgingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReserveId = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristLocations_ImageId",
                table: "TouristLocations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_ReviewId",
                table: "Reserves",
                column: "ReviewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_LodgingId",
                table: "Images",
                column: "LodgingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Reviews_ReviewId",
                table: "Reserves",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristLocations_Images_ImageId",
                table: "TouristLocations",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Reviews_ReviewId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristLocations_Images_ImageId",
                table: "TouristLocations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_TouristLocations_ImageId",
                table: "TouristLocations");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_ReviewId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristLocations");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Reserves");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TouristLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Lodgings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
