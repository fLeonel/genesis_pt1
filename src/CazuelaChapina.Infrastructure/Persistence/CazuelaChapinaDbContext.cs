using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CazuelaChapina.Infrastructure.Persistence;

public class CazuelaChapinaDbContext : DbContext, IAppDbContext
{
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<VentaDetalle> VentaDetalle => Set<VentaDetalle>();
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<RecetaDetalle> RecetaDetalles => Set<RecetaDetalle>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    public CazuelaChapinaDbContext(DbContextOptions<CazuelaChapinaDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dictionaryConverter = new ValueConverter<Dictionary<string, string>?, string?>(
            v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => v == null ? new Dictionary<string, string>() : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!
        );

        modelBuilder.Entity<Producto>()
            .Property(p => p.Atributos)
            .HasConversion(dictionaryConverter);

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

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
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
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}
