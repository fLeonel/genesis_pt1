using CazuelaChapina.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.DeleteBodega;

public class DeleteBodegaCommandHandler : IRequestHandler<DeleteBodegaCommand>
{
    private readonly IAppDbContext _context;

    public DeleteBodegaCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bodegas
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"No se encontró la bodega con ID {request.Id}");

        _context.Bodegas.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
