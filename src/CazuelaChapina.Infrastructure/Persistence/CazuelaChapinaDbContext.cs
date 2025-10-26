using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CazuelaChapina.Infrastructure.Persistence;

public class CazuelaChapinaDbContext : DbContext, IAppDbContext
{
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<VentaDetalle> VentaDetalles => Set<VentaDetalle>();
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<RecetaDetalle> RecetaDetalles => Set<RecetaDetalle>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Bodega> Bodegas => Set<Bodega>();
    public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();

    public CazuelaChapinaDbContext(DbContextOptions<CazuelaChapinaDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dictionaryConverter = new ValueConverter<Dictionary<string, string>, string?>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => string.IsNullOrEmpty(v)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!
        );

        var dictionaryComparer = new ValueComparer<Dictionary<string, string>>(
            (d1, d2) => JsonSerializer.Serialize(d1, (JsonSerializerOptions?)null) == JsonSerializer.Serialize(d2, (JsonSerializerOptions?)null),
            d => JsonSerializer.Serialize(d, (JsonSerializerOptions?)null).GetHashCode(),
            d => d == null ? new Dictionary<string, string>() : new Dictionary<string, string>(d)
        );

        modelBuilder.Entity<Producto>()
            .Property(p => p.Atributos)
            .HasConversion(dictionaryConverter)
            .Metadata.SetValueComparer(dictionaryComparer);

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Combo>()
            .HasMany(c => c.Productos)
            .WithMany();

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Ventas)
            .WithOne(v => v.Cliente)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Venta>()
            .HasMany(v => v.Detalles)
            .WithOne(d => d.Venta)
            .HasForeignKey(d => d.VentaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VentaDetalle>()
            .HasOne(d => d.Producto)
            .WithMany()
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Receta>()
            .HasOne(r => r.Producto)
            .WithOne(p => p.Receta)
            .HasForeignKey<Receta>(r => r.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecetaDetalle>()
            .HasOne(d => d.Receta)
            .WithMany(r => (ICollection<RecetaDetalle>)r.Detalles)
            .HasForeignKey(d => d.RecetaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecetaDetalle>()
            .HasOne(d => d.ProductoIngrediente)
            .WithMany()
            .HasForeignKey(d => d.ProductoIngredienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MovimientoInventario>()
            .HasOne(m => m.Producto)
            .WithMany()
            .HasForeignKey(m => m.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
