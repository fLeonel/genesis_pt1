using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Commands.CreateVenta;

public class CreateVentaHandler
{
    private readonly IAppDbContext _context;

    public CreateVentaHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Venta> Handle(CreateVentaCommand command)
    {
        var cliente = await _context.Clientes.FindAsync(command.ClienteId);
        if (cliente is null)
            throw new Exception("Cliente no encontrado.");

        var detalles = new List<VentaDetalle>();

        foreach (var item in command.Detalles)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == item.ProductoId);

            if (producto != null)
            {
                detalles.Add(new VentaDetalle(producto.Id, item.Cantidad, producto.Precio));
                continue;
            }

            var combo = await _context.Combos
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == item.ProductoId);

            if (combo != null)
            {
                foreach (var productoCombo in combo.Productos)
                {
                    detalles.Add(new VentaDetalle(
                        productoCombo.Id,
                        item.Cantidad,
                        productoCombo.Precio
                    ));
                }

                continue;
            }

            throw new Exception($"No se encontró producto ni combo con ID {item.ProductoId}");
        }

        var venta = new Venta(command.ClienteId, detalles, command.MetodoPago, command.Notas);
        await _context.Ventas.AddAsync(venta);
        await _context.SaveChangesAsync();

        return venta;
    }
}
