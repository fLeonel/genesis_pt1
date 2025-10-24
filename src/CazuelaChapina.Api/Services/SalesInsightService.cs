using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using CazuelaChapina.Application.Common.Interfaces;

public class SalesInsightsService
{
    private readonly IAppDbContext _db;
    private readonly LlmService _llm;

    public SalesInsightsService(IAppDbContext db, LlmService llm)
    {
        _db = db;
        _llm = llm;
    }

    public async Task<string> GenerateInsightAsync()
    {
        var ventas = await _db.Ventas
            .Include(v => v.Detalles)
            .ThenInclude(d => d.Producto)
            .ToListAsync();

        var resumen = ventas
            .GroupBy(v => v.Fecha.Month)
            .Select(g => new
            {
                Mes = g.Key,
                Total = g.Sum(v => v.Total),
                ProductosTop = g.SelectMany(v => v.Detalles)
                    .GroupBy(d => d.Producto.Nombre)
                    .Select(grp => new { Producto = grp.Key, Cantidad = grp.Sum(d => d.Cantidad) })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(5)
            });

        var prompt = $@"
Analizá este historial de ventas mensuales y generá un reporte estratégico.
Datos:
{JsonSerializer.Serialize(resumen, new JsonSerializerOptions { WriteIndented = true })}

Devuelve:
1. Productos más vendidos.
2. Productos que podrían escasear.
3. Sugerencias estacionales (por clima o fechas).
4. Posibles combos nuevos según demanda.
";

        return await _llm.AskAsync(prompt);
    }
}
