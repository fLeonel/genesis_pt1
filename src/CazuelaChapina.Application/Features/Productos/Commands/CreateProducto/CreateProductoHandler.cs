using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Productos.Commands.CreateProducto;

public class CreateProductoHandler
{
    private readonly IAppDbContext _context;

    public CreateProductoHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Producto> Handle(CreateProductoCommand command)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == command.CategoriaId);

        if (categoria == null)
        {
            categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre == "Alimentos");

            if (categoria == null)
            {
                categoria = new Categoria("Alimentos", "Categoría por defecto del sistema");
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
            }
        }

        var producto = new Producto(
            nombre: command.Nombre,
            categoriaId: categoria.Id,
            bodegaId: command.BodegaId,
            precioPublico: command.PrecioPublico,
            costoUnitario: command.CostoUnitario,
            unidadMedida: command.UnidadMedida,
            sePuedeVender: command.SePuedeVender,
            sePuedeComprar: command.SePuedeComprar,
            esFabricado: command.EsFabricado,
            descripcion: command.Descripcion,
            atributos: command.Atributos,
            cantidadDisponible: command.CantidadDisponible
        );

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return producto;
    }
}
