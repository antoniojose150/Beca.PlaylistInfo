using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.PlaylistInfo.API.Migrations
{
    public partial class DataSeed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[] { 2, "linking park lista", "linkin park" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Descripcion", "Nombre", "PlaylistId" },
                values: new object[] { 2, "cancion buenisima", "in the end", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[] { 1, "lista musica para el gimnasio", "Gym" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Descripcion", "Nombre", "PlaylistId" },
                values: new object[] { 1, "cancion favorita", "make a move", 1 });
        }
    }
}
