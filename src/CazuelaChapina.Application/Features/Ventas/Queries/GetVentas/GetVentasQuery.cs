using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Queries.GetVentas;

public class GetVentasQuery
{
    private readonly IAppDbContext _context;

    public GetVentasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Venta>> Handle()
    {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
            .ThenInclude(d => d.Producto)
            .ToListAsync();
    }
}
