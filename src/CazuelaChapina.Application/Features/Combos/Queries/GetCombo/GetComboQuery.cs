using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Queries.GetCombos;

public class GetCombosQuery
{
    private readonly IAppDbContext _context;

    public GetCombosQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> Handle()
    {
        var combos = await _context.Combos
            .Include(c => c.Detalles)
                .ThenInclude(cd => cd.Producto)
                    .ThenInclude(p => p.Receta)
                        .ThenInclude(r => r.Detalles)
                            .ThenInclude(d => d.ProductoIngrediente)
            .AsNoTracking()
            .ToListAsync();

        var resultado = new List<object>();

        foreach (var combo in combos)
        {
            var cantidadesPosibles = new List<decimal>();

            foreach (var detalle in combo.Detalles)
            {
                var producto = detalle.Producto;
                decimal stockProducto;

                if (producto.EsFabricado && producto.Receta is not null)
                {
                    var cantidadesIngredientes = new List<decimal>();

                    foreach (var ingredienteDetalle in producto.Receta.Detalles)
                    {
                        var ingrediente = ingredienteDetalle.ProductoIngrediente;
                        if (ingrediente == null || ingredienteDetalle.CantidadRequerida <= 0)
                        {
                            cantidadesIngredientes.Add(0);
                        }
                        else
                        {
                            cantidadesIngredientes.Add(
                                Math.Floor(ingrediente.CantidadDisponible / ingredienteDetalle.CantidadRequerida)
                            );
                        }
                    }

                    stockProducto = cantidadesIngredientes.Count > 0 ? cantidadesIngredientes.Min() : 0;
                }
                else
                {
                    stockProducto = producto.CantidadDisponible;
                }

                // Calcular stock posible según la cantidad requerida por combo
                var stockPorCombo = Math.Floor((decimal)stockProducto / detalle.CantidadPorCombo);
                cantidadesPosibles.Add(stockPorCombo);
            }

            var stockCombo = cantidadesPosibles.Count > 0 ? cantidadesPosibles.Min() : 0;

            resultado.Add(new
            {
                combo.Id,
                combo.Nombre,
                combo.Descripcion,
                combo.PrecioTotal,
                StockCalculado = stockCombo,
                SePuedeVender = stockCombo > 0,
                Productos = combo.Detalles.Select(d => new
                {
                    d.Producto.Id,
                    d.Producto.Nombre,
                    d.Producto.PrecioPublico,
                    d.Producto.CantidadDisponible,
                    d.CantidadPorCombo
                })
            });
        }

        return resultado;
    }
}
