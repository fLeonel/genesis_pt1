namespace CazuelaChapina.Domain.Entities;

public class Bodega : BaseEntity
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    public Guid? BodegaPadreId { get; private set; }
    public Bodega? BodegaPadre { get; private set; }
    private readonly List<Bodega> _subBodegas = new();
    public IReadOnlyCollection<Bodega> SubBodegas => _subBodegas.AsReadOnly();
    private readonly List<Producto> _productos = new();
    public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();

    private Bodega() { }

    public Bodega(string nombre, string? descripcion = null, Bodega? bodegaPadre = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la bodega no puede estar vacío.");

        Nombre = nombre;
        Descripcion = descripcion;
        BodegaPadre = bodegaPadre;
        BodegaPadreId = bodegaPadre?.Id;
    }

    public void Update(string nombre, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la bodega no puede estar vacío.");

        Nombre = nombre;
        Descripcion = descripcion;
        MarkUpdated();
    }

    public void AgregarSubBodega(Bodega subBodega)
    {
        if (subBodega is null)
            throw new ArgumentNullException(nameof(subBodega));

        _subBodegas.Add(subBodega);
        MarkUpdated();
    }

    public void AgregarProducto(Producto producto)
    {
        if (producto is null)
            throw new ArgumentNullException(nameof(producto));

        _productos.Add(producto);
        MarkUpdated();
    }

    private void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
