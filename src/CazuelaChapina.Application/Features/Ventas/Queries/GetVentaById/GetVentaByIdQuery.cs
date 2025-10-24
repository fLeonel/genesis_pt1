using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Queries.GetVentaById;

public class GetVentaByIdQuery
{
    private readonly IAppDbContext _context;

    public GetVentaByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Venta?> Handle(Guid id)
    {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
            .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}
