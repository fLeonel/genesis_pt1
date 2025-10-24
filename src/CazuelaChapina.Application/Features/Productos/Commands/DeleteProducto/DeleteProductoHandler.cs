using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Productos.Commands.DeleteProducto;

public class DeleteProductoHandler
{
    private readonly IAppDbContext _context;

    public DeleteProductoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Guid id)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        if (producto == null)
            return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }
}
