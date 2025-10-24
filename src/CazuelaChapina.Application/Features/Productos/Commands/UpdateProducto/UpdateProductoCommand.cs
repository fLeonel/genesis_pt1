namespace CazuelaChapina.Application.Features.Productos.Commands.UpdateProducto;

public class UpdateProductoCommand
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Guid? CategoriaId { get; set; }
    public decimal PrecioPublico { get; set; }
    public decimal CostoUnitario { get; set; }
    public string UnidadMedida { get; set; } = "unidad";
    public bool SePuedeVender { get; set; } = true;
    public bool SePuedeComprar { get; set; } = true;
    public bool EsFabricado { get; set; } = false;
    public string? Descripcion { get; set; }
    public Dictionary<string, string>? Atributos { get; set; }
    public decimal CantidadDisponible { get; set; } = 0m;
}
