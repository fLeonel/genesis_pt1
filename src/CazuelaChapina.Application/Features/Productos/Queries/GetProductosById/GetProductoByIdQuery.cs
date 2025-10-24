using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Productos.Queries.GetProductoById;

public class GetProductoByIdQuery
{
    private readonly IAppDbContext _context;

    public GetProductoByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<object?> Handle(Guid id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Receta)
                .ThenInclude(r => r.Detalles)
                    .ThenInclude(d => d.ProductoIngrediente)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (producto is null)
            return null;

        decimal stockDisponible = producto.CantidadDisponible;

        if (producto.EsFabricado && producto.Receta is not null)
        {
            var cantidadesPosibles = new List<decimal>();

            foreach (var detalle in producto.Receta.Detalles)
            {
                var ingrediente = detalle.ProductoIngrediente;
                if (ingrediente.CantidadDisponible <= 0 || detalle.CantidadRequerida <= 0)
                {
                    cantidadesPosibles.Add(0);
                }
                else
                {
                    cantidadesPosibles.Add(Math.Floor(ingrediente.CantidadDisponible / detalle.CantidadRequerida));
                }
            }

            stockDisponible = cantidadesPosibles.Count > 0 ? cantidadesPosibles.Min() : 0;
        }

        return new
        {
            producto.Id,
            producto.Nombre,
            Categoria = producto.Categoria.Nombre,
            producto.Descripcion,
            producto.PrecioPublico,
            producto.CostoUnitario,
            producto.UnidadMedida,
            StockCalculado = stockDisponible,
            producto.SePuedeVender,
            producto.SePuedeComprar,
            producto.EsFabricado,
            producto.Atributos,
            Receta = producto.Receta is null ? null : new
            {
                producto.Receta.Id,
                producto.Receta.Nombre,
                Detalles = producto.Receta.Detalles.Select(d => new
                {
                    ProductoIngrediente = d.ProductoIngrediente.Nombre,
                    d.CantidadRequerida
                })
            }
        };
    }
}
