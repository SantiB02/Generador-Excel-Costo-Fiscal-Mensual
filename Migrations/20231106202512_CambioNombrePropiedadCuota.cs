using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombrePropiedadCuota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InteresPorMes",
                table: "Cuotas",
                newName: "InteresControlSubsidio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InteresControlSubsidio",
                table: "Cuotas",
                newName: "InteresPorMes");
        }
    }
}
