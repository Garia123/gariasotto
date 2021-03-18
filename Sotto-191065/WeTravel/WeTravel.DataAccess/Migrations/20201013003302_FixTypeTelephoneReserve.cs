using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.DataAccess.Migrations
{
    [ExcludeFromCodeCoverageAttribute]
    public partial class FixTypeTelephoneReserve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Reserves",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Telephone",
                table: "Reserves",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
