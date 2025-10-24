using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Clientes.Commands.UpdateCliente;

public class UpdateClienteHandler
{
    private readonly IAppDbContext _context;

    public UpdateClienteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateClienteCommand command)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == command.Id);

        if (cliente is null)
            return false;

        cliente.UpdateCliente(
            command.Nombre,
            command.Telefono,
            command.Correo,
            command.Direccion
        );

        await _context.SaveChangesAsync();
        return true;
    }
}
