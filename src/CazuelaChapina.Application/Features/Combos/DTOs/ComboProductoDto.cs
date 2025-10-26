namespace CazuelaChapina.Application.Features.Combos.DTOs;

/// <summary>
/// Representa un producto dentro de un combo (para creación o actualización)
/// </summary>
public class ComboProductoDto
{
    public Guid ProductoId { get; set; }
    public int CantidadPorCombo { get; set; } = 1;
}
