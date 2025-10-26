namespace CazuelaChapina.Domain.Entities;

using System.Text.Json.Serialization;

public class VentaDetalle : BaseEntity
{
    public Guid VentaId { get; private set; }

    [JsonIgnore]
    public Venta Venta { get; private set; } = null!;
    public Guid ProductoId { get; private set; }
    public Producto Producto { get; private set; } = null!;
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public decimal Subtotal { get; private set; }

    private VentaDetalle() { }

    public VentaDetalle(Guid productoId, int cantidad, decimal precioUnitario)
    {
        ProductoId = productoId;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        CalcularSubtotal();
    }

    private void CalcularSubtotal()
    {
        Subtotal = Cantidad * PrecioUnitario;
    }

    public void ActualizarCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        Cantidad = nuevaCantidad;
        CalcularSubtotal();
    }

    public void ActualizarPrecio(decimal nuevoPrecio)
    {
        if (nuevoPrecio <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero.");

        PrecioUnitario = nuevoPrecio;
        CalcularSubtotal();
    }
}
