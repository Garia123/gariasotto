using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.DataAccess.Migrations
{
    [ExcludeFromCodeCoverageAttribute]
    public partial class UpdateReserveDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "ReserveDescriptions",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ReserveDescriptions",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "ReserveDescriptions",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ReserveDescriptions",
                newName: "description");
        }
    }
}
