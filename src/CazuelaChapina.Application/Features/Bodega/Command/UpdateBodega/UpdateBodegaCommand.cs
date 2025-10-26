using MediatR;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.UpdateBodega;

public record UpdateBodegaCommand(
    Guid Id,
    string Nombre,
    string? Descripcion,
    Guid? BodegaPadreId
) : IRequest;
