namespace CazuelaChapina.Domain.Entities;

public class Combo : BaseEntity
{
    public string Nombre { get; private set; } = null!;
    public string? Descripcion { get; private set; }
    public decimal PrecioTotal { get; private set; }
    public List<Producto> Productos { get; private set; } = new();

    private Combo() { }

    public Combo(string nombre, decimal precioTotal, string? descripcion = null, List<Producto>? productos = null)
    {
        Nombre = nombre;
        PrecioTotal = precioTotal;
        Descripcion = descripcion;
        Productos = productos ?? new List<Producto>();
    }

    public void AgregarProducto(Producto producto)
    {
        Productos.Add(producto);
        RecalcularPrecio();
    }

    private void RecalcularPrecio()
    {
        PrecioTotal = Productos.Sum(p => p.Precio);
    }

    public void Update(string nombre, decimal precioTotal, string? descripcion, List<Producto> productos)
    {
        Nombre = nombre;
        PrecioTotal = precioTotal;
        Descripcion = descripcion;

        Productos.Clear();
        Productos.AddRange(productos);
    }
}
