using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;

namespace CazuelaChapina.Application.Features.Clientes.Commands.CreateCliente;

public class CreateClienteHandler
{
    private readonly IAppDbContext _context;

    public CreateClienteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> Handle(CreateClienteCommand command)
    {
        var cliente = new Cliente(
            command.ClienteCodigo,
            command.Nit,
            command.Nombre,
            command.Telefono,
            command.Correo,
            command.Direccion
        );

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }
}
