namespace CazuelaChapina.Application.Features.Clientes.Commands.CreateCliente;

public class CreateClienteCommand
{
    public string ClienteCodigo { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Nit { get; set; } = "CF";
    public string? Direccion { get; set; }
}
