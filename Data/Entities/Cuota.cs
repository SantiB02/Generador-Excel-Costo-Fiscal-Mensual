using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsidiosClientes.Data.Entities
{
    public class Cuota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("IdPrestamo")]
        public Prestamo Prestamo { get; set; } //una cuota corresponde a un préstamo
        public int IdPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int? NroCuota { get; set; }
        public decimal? SaldoInicialCapital { get; set; }
        public decimal? ValorCuota {  get; set; }
        public decimal? Interes {  get; set; }
        public decimal? AmortizacionCapital { get; set; }
        public decimal? SaldoFinalCapital { get; set; }
        public decimal? InteresControlTNA { get; set; }
        public decimal? InteresControlSubsidio { get; set; }
        public int? CantidadDias { get; set; }
    }
}
