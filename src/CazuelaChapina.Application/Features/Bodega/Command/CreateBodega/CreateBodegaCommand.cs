using CazuelaChapina.Domain.Entities;
using MediatR;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.CreateBodega;

public record CreateBodegaCommand(
    string Nombre,
    string? Descripcion,
    Guid? BodegaPadreId
) : IRequest<Guid>;
