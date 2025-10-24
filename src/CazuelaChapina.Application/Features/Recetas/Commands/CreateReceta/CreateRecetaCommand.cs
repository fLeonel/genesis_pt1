using System.ComponentModel.DataAnnotations;

namespace CazuelaChapina.Application.Features.Recetas.Commands.CreateReceta;

public class CreateRecetaCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    public Guid ProductoId { get; set; }
    public string? Descripcion { get; set; }
    public List<CreateRecetaDetalleDto> Detalles { get; set; } = new();
}

public class CreateRecetaDetalleDto
{
    public Guid ProductoIngredienteId { get; set; }
    public decimal CantidadRequerida { get; set; }
    public string UnidadMedida { get; set; } = "unidad";
}
