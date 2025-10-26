using CazuelaChapina.Domain.Entities;
using CazuelaChapina.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.CreateBodega;

public class CreateBodegaCommandHandler : IRequestHandler<CreateBodegaCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateBodegaCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBodegaCommand request, CancellationToken cancellationToken)
    {
        Bodega? bodegaPadre = null;
        if (request.BodegaPadreId.HasValue)
        {
            bodegaPadre = await _context.Bodegas
                .FirstOrDefaultAsync(b => b.Id == request.BodegaPadreId.Value, cancellationToken);

            if (bodegaPadre is null)
                throw new KeyNotFoundException($"No se encontró la bodega padre con ID {request.BodegaPadreId}");
        }

        var nuevaBodega = new Bodega(
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            bodegaPadre: bodegaPadre
        );

        _context.Bodegas.Add(nuevaBodega);
        await _context.SaveChangesAsync(cancellationToken);

        return nuevaBodega.Id;
    }
}
