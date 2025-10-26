using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Dashboard.Queries.GetDashboard;

public class GetDashboardHandler
{
    private readonly IAppDbContext _context;

    public GetDashboardHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle()
    {
        var hoy = DateTime.UtcNow.Date;
        var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);

        var ventas = await _context.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .AsNoTracking()
            .ToListAsync();

        var ventasDiarias = ventas
            .Where(v => v.Fecha.Date == hoy)
            .Sum(v => v.Total);

        var ventasMensuales = ventas
            .Where(v => v.Fecha >= inicioMes)
            .Sum(v => v.Total);

        var detalles = ventas.SelectMany(v => v.Detalles).ToList();

        var tamalesMasVendidos = detalles
            .Where(d => d.Producto != null)
            .GroupBy(d => d.Producto!.Nombre)
            .Select(g => new
            {
                Nombre = g.Key,
                Cantidad = g.Sum(d => d.Cantidad)
            })
            .OrderByDescending(g => g.Cantidad)
            .Take(5)
            .ToList();

        var bebidasPorHorario = ventas
            .GroupBy(v => v.Fecha.Hour)
            .Select(g => new
            {
                Hora = $"{g.Key:00}:00",
                Ventas = g.Sum(v => v.Total)
            })
            .OrderBy(g => g.Hora)
            .ToList();

        var productos = await _context.Productos.AsNoTracking().ToListAsync();
        var totalPicantes = productos.Count(p =>
            p.Atributos.ContainsKey("picante") &&
            p.Atributos["picante"].ToLower() != "sin"
        );
        var totalNoPicantes = productos.Count - totalPicantes;

        var proporcionPicante = new
        {
            Picante = totalPicantes,
            NoPicante = totalNoPicantes
        };

        var utilidadTotal = detalles
            .Where(d => d.Producto != null)
            .Sum(d => (d.PrecioUnitario - d.Producto!.CostoUnitario) * d.Cantidad);

        decimal desperdicio = 0;
        decimal totalInventario = 0;

        if (_context.Productos != null)
            totalInventario = await _context.Productos.SumAsync(p => p.CantidadDisponible);

        if (_context.MovimientosInventario != null)
        {
            desperdicio = (decimal)await _context.MovimientosInventario
                .Where(m => m.Motivo.ToLower().Contains("merma") ||
                            m.Motivo.ToLower().Contains("desperdicio"))
                .SumAsync(m => m.Cantidad);
        }

        var desperdicioPorcentaje = totalInventario > 0
          ? (decimal)((desperdicio / totalInventario) * 100m)
          : 0m;

        return new
        {
            VentasDiarias = ventasDiarias,
            VentasMensuales = ventasMensuales,
            TamalesMasVendidos = tamalesMasVendidos,
            BebidasPorHorario = bebidasPorHorario,
            ProporcionPicante = proporcionPicante,
            UtilidadTotal = utilidadTotal,
            DesperdicioMateriaPrima = desperdicioPorcentaje
        };
    }
}
