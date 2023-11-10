using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsidiosClientes.Data.Entities
{
    public class Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreCliente { get; set; }
        public string? FechaComunicacionBNA { get; set; }
        public decimal? MontoCredito { get; set; }
        public int? CantidadCuotas { get; set; }
        public decimal? TNA { get; set; }
        public decimal? TEM { get; set; }
        public decimal? MontoCuota { get; set; }
        public string? NroPrestamo { get; set; }
        public ICollection<Cuota> Cuotas { get; set; } //un préstamo tiene muchas cuotas
    }
}
