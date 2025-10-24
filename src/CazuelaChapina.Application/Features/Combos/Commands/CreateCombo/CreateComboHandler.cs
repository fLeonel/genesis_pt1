using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Commands.CreateCombo;

public class CreateComboHandler
{
    private readonly IAppDbContext _context;

    public CreateComboHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Combo> Handle(CreateComboCommand command)
    {
        var productos = await _context.Productos
            .Where(p => command.ProductosIds.Contains(p.Id))
            .ToListAsync();

        var combo = new Combo(command.Nombre, command.PrecioTotal, command.Descripcion, productos);

        _context.Combos.Add(combo);
        await _context.SaveChangesAsync();

        return combo;
    }
}
