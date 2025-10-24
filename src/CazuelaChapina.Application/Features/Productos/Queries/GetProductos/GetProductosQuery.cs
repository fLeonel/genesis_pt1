using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Productos.Queries.GetProductos;

public class GetProductosQuery
{
    private readonly IAppDbContext _context;

    public GetProductosQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<object>> Handle()
    {
        var productos = await _context.Productos
            .Include(p => p.Receta)
                .ThenInclude(r => r.Detalles)
                    .ThenInclude(d => d.ProductoIngrediente)
            .AsNoTracking()
            .ToListAsync();

        var resultado = new List<object>();

        foreach (var producto in productos)
        {
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

            resultado.Add(new
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
                producto.Atributos
            });
        }

        return resultado;
    }
}
