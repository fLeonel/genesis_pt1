namespace CazuelaChapina.Domain.Entities;

public class Producto : BaseEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;
    public string Descripcion { get; private set; } = string.Empty;
    public decimal PrecioPublico { get; private set; }
    public decimal Precio => PrecioPublico;
    public decimal CostoUnitario { get; private set; }
    public decimal CantidadDisponible { get; private set; }
    public string UnidadMedida { get; private set; } = "unidad";
    public bool SePuedeVender { get; private set; } = true;
    public bool SePuedeComprar { get; private set; } = true;
    public bool EsFabricado { get; private set; } = false;
    public Dictionary<string, string>? Atributos { get; private set; }
    public Receta? Receta { get; private set; }

    private Producto() { }

    public Producto(
        string nombre,
        Guid categoriaId,
        decimal precioPublico,
        decimal costoUnitario,
        string unidadMedida,
        bool sePuedeVender = true,
        bool sePuedeComprar = true,
        bool esFabricado = false,
        string? descripcion = null,
        Dictionary<string, string>? atributos = null)
    {
        Nombre = nombre;
        CategoriaId = categoriaId;
        PrecioPublico = precioPublico;
        CostoUnitario = costoUnitario;
        UnidadMedida = unidadMedida;
        SePuedeVender = sePuedeVender;
        SePuedeComprar = sePuedeComprar;
        EsFabricado = esFabricado;
        Descripcion = descripcion ?? string.Empty;
        Atributos = atributos ?? new();
    }

    public void ActualizarPrecioPublico(decimal nuevoPrecio)
    {
        PrecioPublico = nuevoPrecio;
    }

    public void ActualizarCosto(decimal nuevoCosto)
    {
        CostoUnitario = nuevoCosto;
    }

    public void AgregarAtributo(string clave, string valor)
    {
        Atributos ??= new();
        Atributos[clave] = valor;
    }

    public void ActualizarStock(decimal cantidad)
    {
        CantidadDisponible += cantidad;
    }

    public void DescontarStock(decimal cantidad)
    {
        if (CantidadDisponible < cantidad)
            throw new InvalidOperationException($"Stock insuficiente para el producto: {Nombre}");
        CantidadDisponible -= cantidad;
    }

    public void Update(
        string nombre,
        Guid categoriaId,
        decimal precioPublico,
        decimal costoUnitario,
        string unidadMedida,
        bool sePuedeVender,
        bool sePuedeComprar,
        bool esFabricado,
        string? descripcion = null,
        Dictionary<string, string>? atributos = null)
    {
        Nombre = nombre;
        CategoriaId = categoriaId;
        PrecioPublico = precioPublico;
        CostoUnitario = costoUnitario;
        UnidadMedida = unidadMedida;
        SePuedeVender = sePuedeVender;
        SePuedeComprar = sePuedeComprar;
        EsFabricado = esFabricado;
        Descripcion = descripcion ?? string.Empty;
        Atributos = atributos;
    }
}
