namespace CazuelaChapina.Application.Features.Ventas.DTOS;

public class VentaDetalleDto
{
    public Guid ProductoId { get; set; }
    public string ProductoNombre { get; set; } = default!;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
