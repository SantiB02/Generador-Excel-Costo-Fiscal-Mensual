using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsidiosClientes.Data.Entities
{
    public class LineaDeCredito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("IdPrestamo")]
        public int IdPrestamo { get; set; }
        public Prestamo? Prestamo { get; set; }
        public int? Anio { get; set; }
        public ICollection<InteresPorMes> InteresesPorMes { get; set; }
    }
}
