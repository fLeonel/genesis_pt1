using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Recetas.Commands.DeleteReceta;

public class DeleteRecetaHandler
{
    private readonly IAppDbContext _context;

    public DeleteRecetaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Guid id)
    {
        var receta = await _context.Recetas.FirstOrDefaultAsync(r => r.Id == id);
        if (receta == null)
            return false;

        _context.Recetas.Remove(receta);
        await _context.SaveChangesAsync();
        return true;
    }
}
