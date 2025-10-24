using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Clientes.Queries.GetClienteById;

public class GetClienteByIdQuery
{
    private readonly IAppDbContext _context;

    public GetClienteByIdQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> Handle(Guid id)
    {
        return await _context.Clientes.AsNoTracking()
            .Include(c => c.Ventas)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
