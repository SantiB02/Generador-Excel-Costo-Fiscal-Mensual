using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsidiosClientes.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionInteresSubsidiodeCuota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InteresControlSubsidio",
                table: "Cuotas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InteresControlSubsidio",
                table: "Cuotas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
