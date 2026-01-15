using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAdicionalAplicada1.Models;

public class Entrada
{

    [Key]
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string Concepto { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Valor debe ser mayor a 0")]
    public double Total { get; set; }

    [ForeignKey(nameof(EntradaId))]
    public ICollection<EntradaDetalle> Detalles { get; set; } = new List<EntradaDetalle>();
}
