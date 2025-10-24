using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Categorias.Commands.CreateCategoria;

public class CreateCategoriaHandler
{
    private readonly IAppDbContext _context;

    public CreateCategoriaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> Handle(CreateCategoriaCommand command)
    {
        var existe = await _context.Categorias
            .AnyAsync(c => c.Nombre.ToLower() == command.Nombre.ToLower());

        if (existe)
            throw new InvalidOperationException($"Ya existe una categoría con el nombre '{command.Nombre}'.");

        var categoria = new Categoria(command.Nombre, command.Descripcion);

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }
}
