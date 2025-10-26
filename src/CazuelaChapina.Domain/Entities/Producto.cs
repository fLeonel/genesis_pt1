using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CazuelaChapina.Domain.Entities;

public class Producto : BaseEntity
{
    public decimal Precio { get; set; }
    public string Nombre { get; private set; } = string.Empty;
    public Guid? CategoriaId { get; private set; }
    public Categoria? Categoria { get; private set; }
    public Guid? BodegaId { get; private set; }
    public Bodega? Bodega { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;
    public decimal PrecioPublico { get; private set; }
    public decimal CostoUnitario { get; private set; }
    public string UnidadMedida { get; private set; } = "unidad";
    public bool SePuedeVender { get; private set; } = true;
    public bool SePuedeComprar { get; private set; } = true;
    public bool EsFabricado { get; private set; } = false;
    public decimal CantidadDisponible { get; set; } = 0m;
    public Dictionary<string, string> Atributos { get; private set; } = new();
    public Receta? Receta { get; private set; }

    private Producto() { }

    public Producto(
        string nombre,
        Guid? categoriaId,
        Guid? bodegaId,
        decimal precioPublico,
        decimal costoUnitario,
        string unidadMedida,
        bool sePuedeVender = true,
        bool sePuedeComprar = true,
        bool esFabricado = false,
        string? descripcion = null,
        Dictionary<string, string>? atributos = null,
        decimal cantidadDisponible = 0m)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto es obligatorio.");

        Nombre = nombre.Trim();
        CategoriaId = categoriaId;
        BodegaId = bodegaId;
        PrecioPublico = precioPublico >= 0 ? precioPublico : throw new ArgumentException("El precio público no puede ser negativo.");
        CostoUnitario = costoUnitario >= 0 ? costoUnitario : throw new ArgumentException("El costo unitario no puede ser negativo.");
        UnidadMedida = unidadMedida;
        SePuedeVender = sePuedeVender;
        SePuedeComprar = sePuedeComprar;
        EsFabricado = esFabricado;
        Descripcion = descripcion ?? string.Empty;
        Atributos = atributos ?? new();
        CantidadDisponible = cantidadDisponible >= 0 ? cantidadDisponible : 0;
    }

    public void SetStock(decimal nuevoValor)
    {
        if (nuevoValor < 0)
            throw new ArgumentException("El stock no puede ser negativo.");
        CantidadDisponible = nuevoValor;
    }

    public void ActualizarStock(decimal cantidad)
    {
        if (cantidad < 0)
            throw new ArgumentException("La cantidad a agregar debe ser positiva.");
        CantidadDisponible += cantidad;
    }

    public void DescontarStock(decimal cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a descontar debe ser positiva.");
        if (CantidadDisponible < cantidad)
            throw new InvalidOperationException($"Stock insuficiente para el producto: {Nombre}");
        CantidadDisponible -= cantidad;
    }

    public void ActualizarPrecioPublico(decimal nuevoPrecio)
    {
        if (nuevoPrecio < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        PrecioPublico = nuevoPrecio;
    }

    public void ActualizarCosto(decimal nuevoCosto)
    {
        if (nuevoCosto < 0)
            throw new ArgumentException("El costo no puede ser negativo.");
        CostoUnitario = nuevoCosto;
    }

    public void AgregarAtributo(string clave, string valor)
    {
        if (string.IsNullOrWhiteSpace(clave))
            throw new ArgumentException("La clave del atributo es obligatoria.");
        Atributos[clave] = valor;
    }

    public void Update(
        string nombre,
        Guid? categoriaId,
        Guid? bodegaId,
        decimal precioPublico,
        decimal costoUnitario,
        string unidadMedida,
        bool sePuedeVender,
        bool sePuedeComprar,
        bool esFabricado,
        string? descripcion = null,
        Dictionary<string, string>? atributos = null,
        decimal cantidadDisponible = 0m)
    {
        Nombre = nombre;
        CategoriaId = categoriaId;
        BodegaId = bodegaId;
        PrecioPublico = precioPublico;
        CostoUnitario = costoUnitario;
        UnidadMedida = unidadMedida;
        SePuedeVender = sePuedeVender;
        SePuedeComprar = sePuedeComprar;
        EsFabricado = esFabricado;
        Descripcion = descripcion ?? string.Empty;
        Atributos = atributos ?? new();
        CantidadDisponible = cantidadDisponible >= 0 ? cantidadDisponible : CantidadDisponible;
    }
}
