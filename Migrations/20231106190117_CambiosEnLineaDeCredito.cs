using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEnLineaDeCredito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineasDeCredito_Prestamos_IdCliente",
                table: "LineasDeCredito");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "LineasDeCredito",
                newName: "IdPrestamo");

            migrationBuilder.RenameIndex(
                name: "IX_LineasDeCredito_IdCliente",
                table: "LineasDeCredito",
                newName: "IX_LineasDeCredito_IdPrestamo");

            migrationBuilder.AddForeignKey(
                name: "FK_LineasDeCredito_Prestamos_IdPrestamo",
                table: "LineasDeCredito",
                column: "IdPrestamo",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineasDeCredito_Prestamos_IdPrestamo",
                table: "LineasDeCredito");

            migrationBuilder.RenameColumn(
                name: "IdPrestamo",
                table: "LineasDeCredito",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_LineasDeCredito_IdPrestamo",
                table: "LineasDeCredito",
                newName: "IX_LineasDeCredito_IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_LineasDeCredito_Prestamos_IdCliente",
                table: "LineasDeCredito",
                column: "IdCliente",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
