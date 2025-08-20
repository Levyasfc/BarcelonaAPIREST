using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarcelonaAPIREST.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Barcelona" },
                    { 2, "RealMadrid" },
                    { 3, "Milan" }
                });

            migrationBuilder.InsertData(
                table: "Jugadors",
                columns: new[] { "Id", "EquipoId", "Name", "Posicion" },
                values: new object[,]
                {
                    { 1, 1, "LamineYamal", "ED" },
                    { 2, 2, "GonzaloGarcia", "DC" },
                    { 3, 3, "RafaelLeao", "EI" }
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
