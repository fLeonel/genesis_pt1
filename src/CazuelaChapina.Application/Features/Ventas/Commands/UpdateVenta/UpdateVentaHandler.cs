using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Commands.UpdateVenta;

public class UpdateVentaHandler
{
    private readonly IAppDbContext _context;

    public UpdateVentaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateVentaCommand command)
    {
        var venta = await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == command.Id);

        if (venta is null)
            return false;

        if (venta.Estado != "Pendiente")
            throw new InvalidOperationException("Solo se pueden editar ventas en estado Pendiente.");

        _context.VentaDetalles.RemoveRange(venta.Detalles);

        var nuevosDetalles = new List<VentaDetalle>();

        if (command.Detalles is not null && command.Detalles.Any())
        {
            foreach (var item in command.Detalles)
            {
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == item.ProductoId);
                if (producto is null)
                    throw new Exception($"Producto con ID {item.ProductoId} no encontrado.");

                nuevosDetalles.Add(new VentaDetalle(producto.Id, item.Cantidad, producto.PrecioPublico));
            }
        }

        venta.UpdateVenta(command.MetodoPago, command.Notas, nuevosDetalles);

        await _context.SaveChangesAsync();
        return true;
    }
}
