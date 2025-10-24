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
            Id = r.Id,
            Nombre = r.Nombre,
            Descripcion = r.Descripcion,
            ProductoId = r.Producto.Id,
            ProductoNombre = r.Producto.Nombre,
            Detalles = r.Detalles.Select(d => new
            {
                ProductoIngredienteId = d.ProductoIngrediente.Id,
                ProductoIngredienteNombre = d.ProductoIngrediente.Nombre,
                d.CantidadRequerida,
                UnidadMedida = d.UnidadMedida ?? d.ProductoIngrediente.UnidadMedida ?? string.Empty
            })
        });
    }
}
