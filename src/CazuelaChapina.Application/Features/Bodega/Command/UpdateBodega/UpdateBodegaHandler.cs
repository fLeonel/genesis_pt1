using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.UpdateBodega;

public class UpdateBodegaCommandHandler : IRequestHandler<UpdateBodegaCommand>
{
    private readonly IAppDbContext _context;

    public UpdateBodegaCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodegas
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"No se encontró la bodega con ID {request.Id}");

        Bodega? bodegaPadre = null;
        if (request.BodegaPadreId.HasValue)
        {
            bodegaPadre = await _context.Bodegas
                .FirstOrDefaultAsync(b => b.Id == request.BodegaPadreId.Value, cancellationToken);

            if (bodegaPadre == null)
                throw new KeyNotFoundException($"No se encontró la bodega padre con ID {request.BodegaPadreId}");
        }

        entity.Update(request.Nombre, request.Descripcion);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
