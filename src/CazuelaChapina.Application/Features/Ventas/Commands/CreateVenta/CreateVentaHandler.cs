using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Commands.CreateVenta
{
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
                    if (producto.CantidadDisponible < item.Cantidad)
                        throw new Exception($"No hay suficiente stock de {producto.Nombre}");

                    producto.CantidadDisponible -= item.Cantidad;

                    _context.MovimientosInventario.Add(new MovimientoInventario
                    {
                        ProductoId = producto.Id,
                        TipoMovimiento = "Salida",
                        Cantidad = item.Cantidad,
                        Fecha = DateTime.UtcNow,
                        Motivo = "Venta directa"
                    });

                    detalles.Add(new VentaDetalle(producto.Id, item.Cantidad, producto.PrecioPublico));
                    continue;
                }

                var combo = await _context.Combos
                    .Include(c => c.Detalles)
                        .ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(c => c.Id == item.ProductoId);

                if (combo != null)
                {
                    foreach (var detalleCombo in combo.Detalles)
                    {
                        var productoCombo = detalleCombo.Producto;
                        var cantidadTotal = item.Cantidad * detalleCombo.CantidadPorCombo;

                        if (productoCombo.CantidadDisponible < cantidadTotal)
                            throw new Exception(
                                $"No hay suficiente stock de {productoCombo.Nombre} en el combo {combo.Nombre}");

                        productoCombo.CantidadDisponible -= cantidadTotal;

                        _context.MovimientosInventario.Add(new MovimientoInventario
                        {
                            ProductoId = productoCombo.Id,
                            TipoMovimiento = "Salida",
                            Cantidad = cantidadTotal,
                            Fecha = DateTime.UtcNow,
                            Motivo = $"Venta combo {combo.Nombre}"
                        });

                        detalles.Add(new VentaDetalle(
                            productoCombo.Id,
                            cantidadTotal,
                            productoCombo.PrecioPublico
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
}
