using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NroCuota = table.Column<int>(type: "INTEGER", nullable: false),
                    SaldoInicialCapital = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorCuota = table.Column<decimal>(type: "TEXT", nullable: false),
                    Interes = table.Column<decimal>(type: "TEXT", nullable: false),
                    AmortizacionCapital = table.Column<decimal>(type: "TEXT", nullable: false),
                    SaldoFinalCapital = table.Column<decimal>(type: "TEXT", nullable: false),
                    InteresControlTNA = table.Column<decimal>(type: "TEXT", nullable: false),
                    InteresControlSubsidio = table.Column<decimal>(type: "TEXT", nullable: false),
                    CantidadDias = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuotas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCliente = table.Column<string>(type: "TEXT", nullable: true),
                    FechaComunicacionBNA = table.Column<string>(type: "TEXT", nullable: true),
                    MontoCredito = table.Column<decimal>(type: "TEXT", nullable: true),
                    CantidadCuotas = table.Column<int>(type: "INTEGER", nullable: true),
                    TNA = table.Column<decimal>(type: "TEXT", nullable: true),
                    TEM = table.Column<decimal>(type: "TEXT", nullable: true),
                    Cuota = table.Column<decimal>(type: "TEXT", nullable: true),
                    NroPrestamo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineasDeCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCliente = table.Column<int>(type: "INTEGER", nullable: false),
                    RazonSocial = table.Column<string>(type: "TEXT", nullable: false),
                    Anio = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasDeCredito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasDeCredito_Prestamos_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Prestamos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteresPorMes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdLineaDeCredito = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCuota = table.Column<int>(type: "INTEGER", nullable: false),
                    InteresMensual = table.Column<decimal>(type: "TEXT", nullable: false),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteresPorMes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteresPorMes_Cuotas_IdCuota",
                        column: x => x.IdCuota,
                        principalTable: "Cuotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteresPorMes_LineasDeCredito_IdLineaDeCredito",
                        column: x => x.IdLineaDeCredito,
                        principalTable: "LineasDeCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InteresPorMes_IdCuota",
                table: "InteresPorMes",
                column: "IdCuota",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InteresPorMes_IdLineaDeCredito",
                table: "InteresPorMes",
                column: "IdLineaDeCredito");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDeCredito_IdCliente",
                table: "LineasDeCredito",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InteresPorMes");

            migrationBuilder.DropTable(
                name: "Cuotas");

            migrationBuilder.DropTable(
                name: "LineasDeCredito");

            migrationBuilder.DropTable(
                name: "Prestamos");
        }
    }
}
