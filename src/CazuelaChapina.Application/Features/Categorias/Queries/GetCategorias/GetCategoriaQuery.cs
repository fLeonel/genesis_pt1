using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Application.Features.Categorias.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Queries.GetCategorias;

public class GetCategoriasQuery
{
    private readonly IAppDbContext _context;

    public GetCategoriasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaDto>> Handle()
    {
        return await _context.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaDto(
                c.Id,
                c.Nombre,
                c.Descripcion,
                c.CreatedAt
            ))
            .ToListAsync();
    }
}
