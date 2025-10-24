namespace CazuelaChapina.Application.Features.Ventas.Commands.UpdateVenta;

public class UpdateVentaCommand
{
    public Guid Id { get; set; }
    public string? MetodoPago { get; set; }
    public string? Notas { get; set; }
    public List<UpdateVentaDetalleDto> Detalles { get; set; } = new();
}

public class UpdateVentaDetalleDto
{
    public Guid ProductoId { get; set; }
    public int Cantidad { get; set; }
}
