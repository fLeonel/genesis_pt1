namespace CazuelaChapina.Domain.Entities;

public class Receta : BaseEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    public Guid ProductoId { get; private set; }
    public Producto Producto { get; private set; } = null!;
    private readonly List<RecetaDetalle> _detalles = new();
    public IReadOnlyCollection<RecetaDetalle> Detalles => _detalles.AsReadOnly();

    private Receta() { }

    public Receta(string nombre, Guid productoId, string? descripcion = null)
    {
        Nombre = nombre;
        ProductoId = productoId;
        Descripcion = descripcion;
    }

    public void AgregarDetalle(Guid productoIngredienteId, decimal cantidadRequerida, string unidadMedida = "unidad")
    {
        if (_detalles.Any(d => d.ProductoIngredienteId == productoIngredienteId))
            throw new InvalidOperationException("El producto ya está incluido en la receta.");

        _detalles.Add(new RecetaDetalle(Id, productoIngredienteId, cantidadRequerida, unidadMedida));
    }

    public void QuitarDetalle(Guid productoIngredienteId)
    {
        var detalle = _detalles.FirstOrDefault(d => d.ProductoIngredienteId == productoIngredienteId);
        if (detalle != null)
            _detalles.Remove(detalle);
    }

    public void ActualizarDetalle(Guid productoIngredienteId, decimal nuevaCantidad, string nuevaUnidad)
    {
        var detalle = _detalles.FirstOrDefault(d => d.ProductoIngredienteId == productoIngredienteId);
        if (detalle == null)
            throw new InvalidOperationException("El producto no está en la receta.");

        detalle.Actualizar(nuevaCantidad, nuevaUnidad);
    }

    public void Update(string nombre, string? descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
}
