using CazuelaChapina.Application.Features.Combos.DTOs;

namespace CazuelaChapina.Application.Features.Combos.Commands.UpdateCombo;

public class UpdateComboCommand
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal PrecioTotal { get; set; }

    public List<ComboProductoDto> Productos { get; set; } = new();
}
