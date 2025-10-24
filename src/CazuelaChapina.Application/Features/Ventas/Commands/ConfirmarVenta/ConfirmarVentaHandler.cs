using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Commands.ConfirmarVenta;

public class ConfirmarVentaHandler
{
    private readonly IAppDbContext _context;

    public ConfirmarVentaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ConfirmarVentaCommand command)
    {
        var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Id == command.Id);

        if (venta is null)
            return false;

        if (venta.Estado != "Pendiente")
            throw new InvalidOperationException("Solo se pueden confirmar ventas pendientes.");

        venta.ConfirmarVenta();

        _context.Ventas.Update(venta);
        await _context.SaveChangesAsync();

        return true;
    }
}
