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
            .Include(c => c.Detalles)
            .FirstOrDefaultAsync(c => c.Id == command.Id);

        if (combo is null)
            throw new Exception("Combo no encontrado.");

        combo.Update(command.Nombre, command.PrecioTotal, command.Descripcion);

        combo.Detalles.Clear();

        var productosValidos = await _context.Productos
            .Where(p => command.Productos.Select(cp => cp.ProductoId).Contains(p.Id))
            .Select(p => p.Id)
            .ToListAsync();

        foreach (var p in command.Productos)
        {
            if (!productosValidos.Contains(p.ProductoId))
                throw new Exception($"El producto con ID {p.ProductoId} no existe.");

            var cantidad = p.CantidadPorCombo > 0 ? p.CantidadPorCombo : 1;
            combo.AgregarProducto(p.ProductoId, cantidad);
        }

        await _context.SaveChangesAsync();
        return true;
    }
}
