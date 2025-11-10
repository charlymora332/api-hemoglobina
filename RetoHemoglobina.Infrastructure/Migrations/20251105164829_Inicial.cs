using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetoHemoglobina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    TipoAlerta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    TipoGenero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Identificacion = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneroId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Identificacion);
                    table.ForeignKey(
                        name: "FK_Paciente_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<float>(type: "real", nullable: false),
                    AlertaId = table.Column<byte>(type: "tinyint", nullable: false),
                    Identificacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Alertas_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "Alertas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Paciente_Identificacion",
                        column: x => x.Identificacion,
                        principalTable: "Paciente",
                        principalColumn: "Identificacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alertas",
                columns: new[] { "Id", "TipoAlerta" },
                values: new object[,]
                {
                    { (byte)0, "Sin alerta" },
                    { (byte)1, "Nivel bajo" },
                    { (byte)2, "Nivel alto" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "TipoGenero" },
                values: new object[,]
                {
                    { (byte)1, "Masculino" },
                    { (byte)2, "Femenino" }
                });

            migrationBuilder.InsertData(
                table: "Paciente",
                columns: new[] { "Identificacion", "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 101, (byte)1, "Carlos Mora" },
                    { 102, (byte)2, "Laura Ríos" }
                });

            migrationBuilder.InsertData(
                table: "Consultas",
                columns: new[] { "Id", "AlertaId", "Identificacion", "Nivel" },
                values: new object[,]
                {
                    { 1, (byte)0, 101, 14.5f },
                    { 2, (byte)1, 102, 13.2f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_AlertaId",
                table: "Consultas",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_Identificacion",
                table: "Consultas",
                column: "Identificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_GeneroId",
                table: "Paciente",
                column: "GeneroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}