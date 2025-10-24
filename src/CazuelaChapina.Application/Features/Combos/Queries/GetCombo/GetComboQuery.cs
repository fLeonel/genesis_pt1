using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Queries.GetCombos;

public class GetCombosQuery
{
    private readonly IAppDbContext _context;
    public GetCombosQuery(IAppDbContext context) => _context = context;

    public async Task<List<Combo>> Handle()
    {
        return await _context.Combos
            .Include(c => c.Productos)
            .AsNoTracking()
            .ToListAsync();
    }
}
