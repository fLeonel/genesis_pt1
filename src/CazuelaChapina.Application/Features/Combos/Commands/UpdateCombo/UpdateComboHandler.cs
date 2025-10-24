using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Commands.UpdateCombo;

public class UpdateComboHandler
{
    private readonly IAppDbContext _context;

    public UpdateComboHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateComboCommand command)
    {
        var combo = await _context.Combos
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == command.Id);

        if (combo == null)
            return false;

        var nuevosProductos = await _context.Productos
            .Where(p => command.ProductosIds.Contains(p.Id))
            .ToListAsync();

        combo.Update(command.Nombre, command.PrecioTotal, command.Descripcion, nuevosProductos);

        await _context.SaveChangesAsync();
        return true;
    }
}
