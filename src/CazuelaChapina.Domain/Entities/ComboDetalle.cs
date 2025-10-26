using System;

namespace CazuelaChapina.Domain.Entities;

public class ComboDetalle : BaseEntity
{
    public Guid ComboId { get; private set; }
    public Combo Combo { get; private set; } = default!;
    public Guid ProductoId { get; private set; }
    public Producto Producto { get; private set; } = default!;
    public int CantidadPorCombo { get; private set; }

    private ComboDetalle() { }

    public ComboDetalle(Guid productoId, int cantidadPorCombo)
    {
        if (productoId == Guid.Empty)
            throw new ArgumentException("El productoId es obligatorio.");

        if (cantidadPorCombo <= 0)
            throw new ArgumentException("La cantidad por combo debe ser mayor a cero.");

        ProductoId = productoId;
        CantidadPorCombo = cantidadPorCombo;
    }

    public void UpdateCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            throw new ArgumentException("La cantidad por combo debe ser mayor a cero.");

        CantidadPorCombo = nuevaCantidad;
    }
}
