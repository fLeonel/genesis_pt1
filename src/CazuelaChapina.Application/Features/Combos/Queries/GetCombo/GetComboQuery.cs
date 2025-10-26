using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Queries.GetCombos;

public class GetCombosQuery
{
    private readonly IAppDbContext _context;
    public GetCombosQuery(IAppDbContext context) => _context = context;

    public async Task<IEnumerable<object>> Handle()
    {
        var combos = await _context.Combos
            .Include(c => c.Productos)
                .ThenInclude(p => p.Receta)
                    .ThenInclude(r => r.Detalles)
                        .ThenInclude(d => d.ProductoIngrediente)
            .AsNoTracking()
            .ToListAsync();

        var resultado = new List<object>();

        foreach (var combo in combos)
        {
            var cantidadesPosibles = new List<decimal>();

            foreach (var producto in combo.Productos)
            {
                decimal stockProducto;

                if (producto.EsFabricado && producto.Receta is not null)
                {
                    var cantidadesIngredientes = new List<decimal>();

                    foreach (var detalle in producto.Receta.Detalles)
                    {
                        var ingrediente = detalle.ProductoIngrediente;
                        if (ingrediente == null || detalle.CantidadRequerida <= 0)
                        {
                            cantidadesIngredientes.Add(0);
                        }
                        else
                        {
                            cantidadesIngredientes.Add(
                                Math.Floor(ingrediente.CantidadDisponible / detalle.CantidadRequerida)
                            );
                        }
                    }

                    stockProducto = cantidadesIngredientes.Count > 0 ? cantidadesIngredientes.Min() : 0;
                }
                else
                {
                    stockProducto = producto.CantidadDisponible;
                }

                cantidadesPosibles.Add(stockProducto);
            }

            var stockCombo = cantidadesPosibles.Count > 0 ? cantidadesPosibles.Min() : 0;

            resultado.Add(new
            {
                combo.Id,
                combo.Nombre,
                combo.Descripcion,
                combo.PrecioTotal,
                combo.Productos,
                StockCalculado = stockCombo,
                SePuedeVender = stockCombo > 0
            });
        }

        return resultado;
    }
}
