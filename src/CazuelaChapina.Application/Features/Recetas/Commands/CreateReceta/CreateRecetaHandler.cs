using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Recetas.Commands.CreateReceta;

public class CreateRecetaHandler
{
    private readonly IAppDbContext _context;

    public CreateRecetaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Receta> Handle(CreateRecetaCommand command)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == command.ProductoId);
        if (producto == null)
            throw new InvalidOperationException("El producto principal no existe.");

        var receta = new Receta(command.Nombre, command.ProductoId, command.Descripcion);

        foreach (var d in command.Detalles)
        {
            receta.AgregarDetalle(d.ProductoIngredienteId, d.CantidadRequerida, d.UnidadMedida);
        }

        _context.Recetas.Add(receta);
        await _context.SaveChangesAsync();

        return receta;
    }
}
