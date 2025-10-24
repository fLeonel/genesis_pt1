using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Clientes.Commands.DeleteCliente;

public class DeleteClienteHandler
{
    private readonly IAppDbContext _context;

    public DeleteClienteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteClienteCommand command)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == command.Id);

        if (cliente is null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}
