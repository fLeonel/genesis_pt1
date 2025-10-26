using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Producto> Productos { get; }
    DbSet<Categoria> Categorias { get; }
    DbSet<Combo> Combos { get; }
    DbSet<ComboDetalle> ComboDetalles { get; }
    DbSet<Cliente> Clientes { get; }
    DbSet<Venta> Ventas { get; }
    DbSet<VentaDetalle> VentaDetalles { get; }
    DbSet<Receta> Recetas { get; }
    DbSet<RecetaDetalle> RecetaDetalles { get; }
    DbSet<Bodega> Bodegas { get; }
    DbSet<MovimientoInventario> MovimientosInventario { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
