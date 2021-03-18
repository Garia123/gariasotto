using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTravel.DataAccess.Migrations
{
    [ExcludeFromCodeCoverageAttribute]
    public partial class ReserveUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_TouristLocations_TouristLocationId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristLocations_Regions_RegionId",
                table: "TouristLocations");

            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "NumberOfBabies",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "NumberOfChilds",
                table: "Reserves");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "TouristLocations",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Reserves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Babies",
                table: "Reserves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Reserves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Regions",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristLocationId",
                table: "Lodgings",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_TouristLocations_TouristLocationId",
                table: "Lodgings",
                column: "TouristLocationId",
                principalTable: "TouristLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristLocations_Regions_RegionId",
                table: "TouristLocations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_TouristLocations_TouristLocationId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristLocations_Regions_RegionId",
                table: "TouristLocations");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Babies",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Regions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "TouristLocations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBabies",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChilds",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "TouristLocationId",
                table: "Lodgings",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_TouristLocations_TouristLocationId",
                table: "Lodgings",
                column: "TouristLocationId",
                principalTable: "TouristLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristLocations_Regions_RegionId",
                table: "TouristLocations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
