namespace CazuelaChapina.Application.Features.Recetas.Commands.UpdateReceta;

public class UpdateRecetaCommand
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public List<UpdateRecetaDetalleDto> Detalles { get; set; } = new();
}

public class UpdateRecetaDetalleDto
{
    public Guid ProductoIngredienteId { get; set; }
    public decimal CantidadRequerida { get; set; }
    public string UnidadMedida { get; set; } = "unidad";
}
