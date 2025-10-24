namespace CazuelaChapina.Application.Features.Clientes.Commands.UpdateCliente;

public class UpdateClienteCommand
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
}
