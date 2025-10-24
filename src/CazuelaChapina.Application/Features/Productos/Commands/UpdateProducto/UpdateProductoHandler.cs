using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Productos.Commands.UpdateProducto;

public class UpdateProductoHandler
{
    private readonly IAppDbContext _context;

    public UpdateProductoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductoCommand command)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == command.Id);
        if (producto is null) return false;

        producto.Update(
            command.Nombre,
            command.CategoriaId,
            command.PrecioPublico,
            command.CostoUnitario,
            command.UnidadMedida,
            command.SePuedeVender,
            command.SePuedeComprar,
            command.EsFabricado,
            command.Descripcion,
            command.Atributos,
            command.CantidadDisponible
        );

        await _context.SaveChangesAsync();
        return true;
    }
}
