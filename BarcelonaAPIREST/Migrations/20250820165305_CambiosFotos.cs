using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarcelonaAPIREST.Migrations
{
    /// <inheritdoc />
    public partial class CambiosFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Jugadors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Escudo",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Escudo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Escudo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Escudo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jugadors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Foto",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jugadors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Foto",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jugadors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Foto",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Jugadors");

            migrationBuilder.DropColumn(
                name: "Escudo",
                table: "Equipos");
        }
    }
}
