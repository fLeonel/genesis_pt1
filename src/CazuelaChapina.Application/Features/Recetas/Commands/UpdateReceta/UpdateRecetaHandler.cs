using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Recetas.Commands.UpdateReceta;

public class UpdateRecetaHandler
{
    private readonly IAppDbContext _context;

    public UpdateRecetaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateRecetaCommand command)
    {
        var receta = await _context.Recetas
            .Include(r => r.Detalles)
            .FirstOrDefaultAsync(r => r.Id == command.Id);

        if (receta == null)
            return false;

        receta.Update(command.Nombre, command.Descripcion);

        var existentes = receta.Detalles.ToList();
        foreach (var e in existentes)
            receta.QuitarDetalle(e.ProductoIngredienteId);

        foreach (var d in command.Detalles)
            receta.AgregarDetalle(d.ProductoIngredienteId, d.CantidadRequerida, d.UnidadMedida);

        await _context.SaveChangesAsync();
        return true;
    }
}
