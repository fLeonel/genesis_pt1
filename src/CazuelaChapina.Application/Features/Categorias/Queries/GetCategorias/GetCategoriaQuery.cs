using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Queries.GetCategorias;

public class GetCategoriasQuery
{
    private readonly IAppDbContext _context;

    public GetCategoriasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> Handle()
    {
        return await _context.Categorias
            .AsNoTracking()
            .Include(c => c.Productos)
            .ToListAsync();
    }
}
