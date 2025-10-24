namespace CazuelaChapina.Application.Features.Ventas.Commands.CreateVenta;

public class CreateVentaCommand
{
    public Guid ClienteId { get; set; }
    public string MetodoPago { get; set; } = "Efectivo";
    public string? Notas { get; set; }
    public List<CreateVentaDetalleDto> Detalles { get; set; } = new();
}

public class CreateVentaDetalleDto
{
    public Guid ProductoId { get; set; }
    public int Cantidad { get; set; }
}
