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
        if (string.IsNullOrWhiteSpace(command.Nombre))
            throw new ArgumentException("El nombre del combo es obligatorio.");

        var combo = new Combo(command.Nombre, command.PrecioTotal, command.Descripcion);

        if (command.Productos != null && command.Productos.Any())
        {
            foreach (var item in command.Productos)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto == null) continue;

                combo.AgregarProducto(item.ProductoId, item.Cantidad);
            }
        }

        await _context.Combos.AddAsync(combo);
        await _context.SaveChangesAsync();

        return combo;
    }
}
