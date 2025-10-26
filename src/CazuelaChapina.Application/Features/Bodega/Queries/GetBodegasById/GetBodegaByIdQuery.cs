using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Bodegas.Queries.GetBodegaById;

public class GetBodegaByIdQuery
{
    private readonly IAppDbContext _context;

    public GetBodegaByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Bodega?> Handle(Guid id)
    {
        return await _context.Bodegas.AsNoTracking()
            .Include(b => b.SubBodegas)
            .Include(b => b.Productos)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
