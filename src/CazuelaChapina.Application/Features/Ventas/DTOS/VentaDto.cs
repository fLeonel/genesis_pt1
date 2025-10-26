namespace CazuelaChapina.Application.Features.Ventas.DTOS;


public class VentaDto
{
    public Guid Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string? Notas { get; set; }

    public Guid ClienteId { get; set; }
    public string ClienteNombre { get; set; } = default!;
    public string ClienteNit { get; set; } = default!;

    public List<VentaDetalleDto> Detalles {get; set; } = new();
}
