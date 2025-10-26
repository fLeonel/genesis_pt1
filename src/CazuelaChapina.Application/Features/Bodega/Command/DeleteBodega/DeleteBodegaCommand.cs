using MediatR;

namespace CazuelaChapina.Application.Features.Bodegas.Commands.DeleteBodega;

public record DeleteBodegaCommand(Guid Id) : IRequest;
