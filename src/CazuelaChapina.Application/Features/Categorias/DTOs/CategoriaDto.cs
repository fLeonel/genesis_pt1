namespace CazuelaChapina.Application.Features.Categorias.DTOs;

public record CategoriaDto(
    Guid Id,
    string Nombre,
    string? Descripcion,
    DateTime CreatedAt
);

