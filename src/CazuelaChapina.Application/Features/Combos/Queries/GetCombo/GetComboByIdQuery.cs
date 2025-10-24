using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Queries.GetComboById;

public class GetComboByIdQuery
{
    private readonly IAppDbContext _context;
    public GetComboByIdQuery(IAppDbContext context) => _context = context;

    public async Task<Combo?> Handle(Guid id)
    {
        return await _context.Combos
            .Include(c => c.Productos)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
