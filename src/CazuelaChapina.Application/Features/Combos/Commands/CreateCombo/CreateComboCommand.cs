using CazuelaChapina.Application.Features.Combos.DTOs;

namespace CazuelaChapina.Application.Features.Combos.Commands.CreateCombo;

public class CreateComboCommand
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal PrecioTotal { get; set; }
    public List<CreateComboProductoDto> Productos { get; set; } = new();
}
