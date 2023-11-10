using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    /// <inheritdoc />
    public partial class SimplificacionDeEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InteresPorMes");

            migrationBuilder.DropTable(
                name: "LineasDeCredito");

            migrationBuilder.RenameColumn(
                name: "Cuota",
                table: "Prestamos",
                newName: "MontoCuota");

            migrationBuilder.AddColumn<int>(
                name: "IdPrestamo",
                table: "Cuotas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "InteresPorMes",
                table: "Cuotas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Cuotas_IdPrestamo",
                table: "Cuotas",
                column: "IdPrestamo");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuotas_Prestamos_IdPrestamo",
                table: "Cuotas",
                column: "IdPrestamo",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuotas_Prestamos_IdPrestamo",
                table: "Cuotas");

            migrationBuilder.DropIndex(
                name: "IX_Cuotas_IdPrestamo",
                table: "Cuotas");

            migrationBuilder.DropColumn(
                name: "IdPrestamo",
                table: "Cuotas");

            migrationBuilder.DropColumn(
                name: "InteresPorMes",
                table: "Cuotas");

            migrationBuilder.RenameColumn(
                name: "MontoCuota",
                table: "Prestamos",
                newName: "Cuota");

            migrationBuilder.CreateTable(
                name: "LineasDeCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPrestamo = table.Column<int>(type: "INTEGER", nullable: false),
                    Anio = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasDeCredito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasDeCredito_Prestamos_IdPrestamo",
                        column: x => x.IdPrestamo,
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
                    IdCuota = table.Column<int>(type: "INTEGER", nullable: false),
                    IdLineaDeCredito = table.Column<int>(type: "INTEGER", nullable: false),
                    InteresMensual = table.Column<decimal>(type: "TEXT", nullable: true),
                    Mes = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "IX_LineasDeCredito_IdPrestamo",
                table: "LineasDeCredito",
                column: "IdPrestamo");
        }
    }
}
