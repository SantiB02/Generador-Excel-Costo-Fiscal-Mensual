using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsidiosClientes.Data.Entities
{
    public class InteresPorMes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("IdLineaDeCredito")]
        public int IdLineaDeCredito { get; set; }
        public LineaDeCredito LineaDeCredito { get; set;}
        [ForeignKey("IdCuota")]
        public int IdCuota { get; set; }
        public Cuota Cuota { get; set; }
        public decimal? InteresMensual {  get; set; }
        public int? Mes {  get; set; }
    }
}
