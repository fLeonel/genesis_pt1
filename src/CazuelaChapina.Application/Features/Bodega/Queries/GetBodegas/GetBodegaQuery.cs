using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Bodegas.Queries.GetBodegas;

public class GetBodegasQuery
{
    private readonly IAppDbContext _context;

    public GetBodegasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Bodega>> Handle()
    {
        return await _context.Bodegas
            .Include(b => b.SubBodegas)
            .AsNoTracking()
            .ToListAsync();
    }
}
