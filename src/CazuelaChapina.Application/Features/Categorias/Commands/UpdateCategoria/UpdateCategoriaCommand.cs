namespace CazuelaChapina.Application.Features.Categorias.Commands.UpdateCategoria;

public class UpdateCategoriaCommand
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}
