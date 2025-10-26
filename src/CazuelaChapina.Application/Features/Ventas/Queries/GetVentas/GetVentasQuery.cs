using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Application.Features.Ventas.DTOS;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Ventas.Queries.GetVentas;

public class GetVentasQuery
{
    private readonly IAppDbContext _context;

    public GetVentasQuery(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VentaDto>> Handle()
    {
        return await _context.Ventas
            .AsNoTracking()
            .Select(v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                MetodoPago = v.MetodoPago,
                Estado = v.Estado,
                Notas = v.Notas,
                ClienteId = v.ClienteId,
                ClienteNombre = v.Cliente.Nombre,
                ClienteNit = v.Cliente.Nit,
                Detalles = v.Detalles.Select(d => new VentaDetalleDto
                {
                    ProductoId = d.ProductoId,
                    ProductoNombre = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            })
            .ToListAsync();
    }
}
