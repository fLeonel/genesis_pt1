using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Recetas.Queries.GetRecetaById;

public class GetRecetaByIdQuery
{
    private readonly IAppDbContext _context;

    public GetRecetaByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<object?> Handle(Guid id)
    {
        var receta = await _context.Recetas
            .Include(r => r.Producto)
            .Include(r => r.Detalles)
                .ThenInclude(d => d.ProductoIngrediente)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (receta == null)
            return null;

        return new
        {
            Id = receta.Id,
            Nombre = receta.Nombre,
            Descripcion = receta.Descripcion,
            ProductoId = receta.Producto.Id,
            ProductoNombre = receta.Producto.Nombre,
            Detalles = receta.Detalles.Select(d => new
            {
                ProductoIngredienteId = d.ProductoIngrediente.Id,
                ProductoIngredienteNombre = d.ProductoIngrediente.Nombre,
                d.CantidadRequerida,
                UnidadMedida = d.UnidadMedida ?? d.ProductoIngrediente.UnidadMedida ?? string.Empty
            })
        };
    }
}
