namespace CazuelaChapina.Application.Features.Ventas.Commands.ConfirmarVenta;

public class ConfirmarVentaCommand
{
    public Guid Id { get; set; }

    public ConfirmarVentaCommand(Guid id)
    {
        Id = id;
    }

    public ConfirmarVentaCommand() { }
}
