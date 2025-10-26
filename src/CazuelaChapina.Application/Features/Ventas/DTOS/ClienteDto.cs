namespace CazuelaChapina.Application.Features.Ventas.DTOS;

public class ClienteDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Nit { get; set; } = default!;
}
