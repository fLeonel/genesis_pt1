using System;
using System.Collections.Generic;
using System.Linq;

namespace CazuelaChapina.Domain.Entities;

public class Combo : BaseEntity
{
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    public decimal PrecioTotal { get; private set; }
    public List<ComboDetalle> Detalles { get; private set; } = new();

    private Combo() { }

    public Combo(string nombre, decimal precioTotal, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del combo es obligatorio.");

        if (precioTotal < 0)
            throw new ArgumentException("El precio total no puede ser negativo.");

        Nombre = nombre.Trim();
        PrecioTotal = precioTotal;
        Descripcion = descripcion ?? string.Empty;
    }

    public void AgregarProducto(Guid productoId, int cantidadPorCombo)
    {
        if (productoId == Guid.Empty)
            throw new ArgumentException("El productoId es obligatorio.");

        if (cantidadPorCombo <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        var existente = Detalles.FirstOrDefault(d => d.ProductoId == productoId);
        if (existente != null)
        {
            existente.UpdateCantidad(existente.CantidadPorCombo + cantidadPorCombo);
        }
        else
        {
            var detalle = new ComboDetalle(productoId, cantidadPorCombo);
            Detalles.Add(detalle);
        }
    }

    public void QuitarProducto(Guid productoId)
    {
        var detalle = Detalles.FirstOrDefault(d => d.ProductoId == productoId);
        if (detalle != null)
            Detalles.Remove(detalle);
    }

    public void Update(string nombre, decimal precioTotal, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (precioTotal < 0)
            throw new ArgumentException("El precio no puede ser negativo.");

        Nombre = nombre.Trim();
        PrecioTotal = precioTotal;
        Descripcion = descripcion ?? string.Empty;
    }
}
