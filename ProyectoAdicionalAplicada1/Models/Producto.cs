using System.ComponentModel.DataAnnotations;

namespace ProyectoAdicionalAplicada1.Models;

public class Producto
{

    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Valor debe ser mayor a 0")]
    public double Costo { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Valor debe ser mayor a 0")]
    public double Precio { get; set; }

    public int Existencia { get; set; }

}
