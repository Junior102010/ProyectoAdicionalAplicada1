using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAdicionalAplicada1.Models;

public class EntradaDetalle
{
    [Key]
    public int Id { get; set; }

    public int EntradaId { get; set; }

    [ForeignKey("ProductoId")]
    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto")]
    public int? ProductoId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Valor debe ser mayor a 0")]
    public double Costo { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, int.MaxValue, ErrorMessage = "El Valor debe ser mayor a 0")]
    public int Cantidad { get; set; }
}
