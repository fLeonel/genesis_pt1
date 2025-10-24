using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Commands.UpdateCategoria;

public class UpdateCategoriaHandler
{
    private readonly IAppDbContext _context;

    public UpdateCategoriaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateCategoriaCommand command)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == command.Id);

        if (categoria == null)
            return false;

        categoria.Update(command.Nombre, command.Descripcion);
        await _context.SaveChangesAsync();

        return true;
    }
}
