using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Commands.DeleteVenta;

public class DeleteVentaHandler
{
    private readonly IAppDbContext _context;

    public DeleteVentaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteVentaCommand command)
    {
        var venta = await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == command.Id);

        if (venta is null) return false;

        _context.Ventas.Remove(venta);
        await _context.SaveChangesAsync();

        return true;
    }
}
