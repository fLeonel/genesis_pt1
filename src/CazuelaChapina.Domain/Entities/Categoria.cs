namespace CazuelaChapina.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    private readonly List<Producto> _productos = new();
    public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();

    private Categoria() { }

    public Categoria(string nombre, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

        Nombre = nombre;
        Descripcion = descripcion;
    }

    public void Update(string nombre, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

        Nombre = nombre;
        Descripcion = descripcion;
    }

    public void AgregarProducto(Producto producto)
    {
        if (producto is null)
            throw new ArgumentNullException(nameof(producto));

        _productos.Add(producto);
    }
}
