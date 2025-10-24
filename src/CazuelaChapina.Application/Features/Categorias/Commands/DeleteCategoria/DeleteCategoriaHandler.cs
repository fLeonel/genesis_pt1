using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Commands.DeleteCategoria;

public class DeleteCategoriaHandler
{
    private readonly IAppDbContext _context;

    public DeleteCategoriaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCategoriaCommand command)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == command.Id);

        if (categoria == null)
            return false;

        if (categoria.Productos.Any())
            throw new InvalidOperationException("No se puede eliminar una categoría que tiene productos asociados.");

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return true;
    }
}
