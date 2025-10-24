using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Queries.GetCategoriaById;

public class GetCategoriaByIdQuery
{
    private readonly IAppDbContext _context;

    public GetCategoriaByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> Handle(Guid id)
    {
        return await _context.Categorias
            .AsNoTracking()
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
