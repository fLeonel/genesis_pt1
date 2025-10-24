using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Clientes.Queries.GetClientes;

public class GetClientesQuery
{
    private readonly IAppDbContext _context;

    public GetClientesQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> Handle()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }
}
