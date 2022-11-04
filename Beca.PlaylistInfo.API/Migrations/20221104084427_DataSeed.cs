using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.PlaylistInfo.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlaylistId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlaylistId",
                value: 0);
        }
    }
}
