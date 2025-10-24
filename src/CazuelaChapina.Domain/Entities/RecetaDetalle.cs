namespace CazuelaChapina.Domain.Entities;

public class RecetaDetalle : BaseEntity
{
    public Guid RecetaId { get; private set; }
    public Receta Receta { get; private set; } = null!;
    public Guid ProductoIngredienteId { get; private set; }
    public Producto ProductoIngrediente { get; private set; } = null!;
    public decimal CantidadRequerida { get; private set; }
    public string UnidadMedida { get; private set; } = "unidad";

    private RecetaDetalle() { }

    public RecetaDetalle(Guid recetaId, Guid productoIngredienteId, decimal cantidadRequerida, string unidadMedida = "unidad")
    {
        RecetaId = recetaId;
        ProductoIngredienteId = productoIngredienteId;
        CantidadRequerida = cantidadRequerida;
        UnidadMedida = unidadMedida;
    }

    public void Actualizar(decimal nuevaCantidad, string nuevaUnidad)
    {
        if (nuevaCantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        CantidadRequerida = nuevaCantidad;
        UnidadMedida = nuevaUnidad;
    }
}
