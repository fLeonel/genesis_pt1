using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Application.Features.Categorias.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Commands.UpdateCategoria;

public class UpdateCategoriaHandler
{
    private readonly IAppDbContext _context;

    public UpdateCategoriaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CategoriaDto?> Handle(UpdateCategoriaCommand command)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == command.Id);
        if (categoria is null)
            return null;

        categoria.Update(command.Nombre, command.Descripcion);
        await _context.SaveChangesAsync();

        return new CategoriaDto(categoria.Id, categoria.Nombre, categoria.Descripcion, categoria.CreatedAt);
    }
}
