using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Recetas.Queries.GetRecetas;

public class GetRecetasQuery
{
    private readonly IAppDbContext _context;

    public GetRecetasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> Handle()
    {
        var recetas = await _context.Recetas
            .Include(r => r.Producto)
            .Include(r => r.Detalles)
                .ThenInclude(d => d.ProductoIngrediente)
            .ToListAsync();

        return recetas.Select(r => new
        {
            r.Id,
            r.Nombre,
            r.Descripcion,
            ProductoPrincipal = r.Producto.Nombre,
            Detalles = r.Detalles.Select(d => new
            {
                ProductoIngrediente = d.ProductoIngrediente.Nombre,
                d.CantidadRequerida
            })
        });
    }
}
