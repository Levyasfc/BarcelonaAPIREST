using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarcelonaAPIREST.Migrations
{
    /// <inheritdoc />
    public partial class BaseBarcelona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escudo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugadors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dorsal = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugadors_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipos",
                columns: new[] { "Id", "Escudo", "Name" },
                values: new object[,]
                {
                    { 1, null, "Barcelona" },
                    { 2, null, "RealMadrid" },
                    { 3, null, "Milan" }
                });

            migrationBuilder.InsertData(
                table: "Jugadors",
                columns: new[] { "Id", "Dorsal", "EquipoId", "Foto", "Name", "Posicion" },
                values: new object[,]
                {
                    { 1, 10, 1, "https://www.fcbarcelona.com/photo-resources/2025/07/09/47ddeac8-9a36-4618-af27-5b1310184370/19-Lamine.jpg?width=1200&height=750", "LamineYamal", "ED" },
                    { 2, 14, 1, "https://www.fcbarcelona.com/photo-resources/2025/07/28/75f02288-6747-4d91-8a1f-fcbd3857fd1a/Fitxa-14-Blau-Rashford.jpg?width=1680&height=1050", "MarcusRashford", "EI" },
                    { 3, 9, 1, "https://www.fcbarcelona.com/photo-resources/2025/07/09/e8378527-14fe-4dc7-a0c2-e3cea291a6e2/09-Lewandowski.jpg?width=1680&height=1050", "RobertLewandoski", "DC" },
                    { 4, 11, 1, "https://www.fcbarcelona.com/photo-resources/2025/07/09/87585699-3a8f-41d4-9912-e63f569f6fd6/11-Raphinha.jpg?width=1680&height=1050", "Raphinha", "EI" },
                    { 5, 13, 1, "https://www.fcbarcelona.com/photo-resources/2025/07/09/3337817d-b39f-4dfa-924b-89994d15eee1/00-Joan_Garcia.jpg?width=1680&height=1050", "JoanGarcia", "POR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jugadors_EquipoId",
                table: "Jugadors",
                column: "EquipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jugadors");

            migrationBuilder.DropTable(
                name: "Equipos");
        }
    }
}
