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

        if (command.BodegaId.HasValue)
        {
            var bodega = await _context.Bodegas.FirstOrDefaultAsync(b => b.Id == command.BodegaId);
            if (bodega == null)
                throw new KeyNotFoundException($"No se encontró la bodega con ID {command.BodegaId}");
        }

        producto.Update(
            command.Nombre,
            command.CategoriaId,
            command.BodegaId,
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
