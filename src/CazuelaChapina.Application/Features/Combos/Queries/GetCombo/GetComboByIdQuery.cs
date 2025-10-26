using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Queries.GetComboById;

public class GetComboByIdQuery
{
    private readonly IAppDbContext _context;
    public GetComboByIdQuery(IAppDbContext context) => _context = context;

    public async Task<object?> Handle(Guid id)
    {
        var combo = await _context.Combos
            .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (combo is null)
            return null;

        return new
        {
            combo.Id,
            combo.Nombre,
            combo.Descripcion,
            combo.PrecioTotal,
            Productos = combo.Detalles.Select(d => new
            {
                d.Producto.Id,
                d.Producto.Nombre,
                d.Producto.Descripcion,
                d.Producto.PrecioPublico,
                d.Producto.CantidadDisponible,
                d.Producto.UnidadMedida,
                CantidadPorCombo = d.CantidadPorCombo
            }).ToList()
        };
    }
}
