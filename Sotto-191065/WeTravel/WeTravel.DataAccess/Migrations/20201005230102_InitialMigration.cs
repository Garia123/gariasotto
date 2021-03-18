using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTravel.DataAccess.Migrations
{
    [ExcludeFromCodeCoverageAttribute]
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TouristLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TouristLocations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Stars = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Images = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PricePerNight = table.Column<double>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    TouristLocationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lodgings_TouristLocations_TouristLocationId",
                        column: x => x.TouristLocationId,
                        principalTable: "TouristLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TouristLocationCategories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    TouristLocationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristLocationCategories", x => new { x.TouristLocationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TouristLocationCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristLocationCategories_TouristLocations_TouristLocationId",
                        column: x => x.TouristLocationId,
                        principalTable: "TouristLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CheckIn = table.Column<DateTime>(nullable: false),
                    CheckOut = table.Column<DateTime>(nullable: false),
                    NumberOfAdults = table.Column<int>(nullable: false),
                    NumberOfChilds = table.Column<int>(nullable: false),
                    NumberOfBabies = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Telephone = table.Column<int>(nullable: false),
                    InformationText = table.Column<string>(nullable: true),
                    ContactFirstName = table.Column<string>(nullable: false),
                    ContactLastName = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    LodgingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserves_Lodgings_LodgingId",
                        column: x => x.LodgingId,
                        principalTable: "Lodgings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveDescriptions",
                columns: table => new
                {
                    ReserveId = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    state = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveDescriptions", x => x.ReserveId);
                    table.ForeignKey(
                        name: "FK_ReserveDescriptions_Reserves_ReserveId",
                        column: x => x.ReserveId,
                        principalTable: "Reserves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_TouristLocationId",
                table: "Lodgings",
                column: "TouristLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_LodgingId",
                table: "Reserves",
                column: "LodgingId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristLocationCategories_CategoryId",
                table: "TouristLocationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristLocations_RegionId",
                table: "TouristLocations",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveDescriptions");

            migrationBuilder.DropTable(
                name: "TouristLocationCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Reserves");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Lodgings");

            migrationBuilder.DropTable(
                name: "TouristLocations");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
