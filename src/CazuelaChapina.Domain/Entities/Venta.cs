namespace CazuelaChapina.Domain.Entities;

public class Venta : BaseEntity
{
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    public DateTime Fecha { get; private set; } = DateTime.UtcNow;
    public decimal Total { get; private set; }
    public string MetodoPago { get; private set; } = "Efectivo";
    public string Estado { get; private set; } = "Pendiente";
    //agrego esto para saber cuales estados manejare: Pendiente, Confirmada, Cancelada, Entregada
    public string? Notas { get; private set; }

    private readonly List<VentaDetalle> _detalles = new();
    public IReadOnlyCollection<VentaDetalle> Detalles => _detalles.AsReadOnly();

    private Venta() { }

    public Venta(Guid clienteId, List<VentaDetalle> detalles, string metodoPago = "Efectivo", string? notas = null)
    {
        ClienteId = clienteId;
        MetodoPago = metodoPago;
        Notas = notas;
        _detalles = detalles ?? new List<VentaDetalle>();
        CalcularTotal();
    }

    private void CalcularTotal()
    {
        Total = _detalles.Sum(d => d.Subtotal);
    }

    public void ConfirmarVenta()
    {
        if (Estado != "Pendiente")
            throw new InvalidOperationException("Solo las ventas pendientes pueden ser confirmadas.");

        Estado = "Confirmada";
    }

    public void CancelarVenta()
    {
        if (Estado == "Entregada")
            throw new InvalidOperationException("No se puede cancelar una venta que ya fue entregada.");

        Estado = "Cancelada";
    }

    public void MarcarComoEntregada()
    {
        if (Estado != "Confirmada")
            throw new InvalidOperationException("Solo las ventas confirmadas pueden marcarse como entregadas.");

        Estado = "Entregada";
    }

    public void AgregarNota(string nota)
    {
        if (!string.IsNullOrWhiteSpace(nota))
            Notas = $"{Notas}\n{nota}".Trim();
    }

    public void CambiarEstado(string nuevoEstado)
    {
        if (Estado == "Entregada" || Estado == "Cancelada")
            throw new InvalidOperationException("No se puede modificar una venta que ya está cerrada.");

        Estado = nuevoEstado;
    }

    public void ActualizarPago(string nuevoMetodoPago)
    {
        if (Estado != "Pendiente")
            throw new InvalidOperationException("Solo las ventas pendientes pueden cambiar su método de pago.");

        MetodoPago = nuevoMetodoPago;
    }

    // ✅ Este es el método que faltaba:
    public void UpdateVenta(string metodoPago, string? notas, List<VentaDetalle>? nuevosDetalles)
    {
        if (Estado != "Pendiente")
            throw new InvalidOperationException("Solo las ventas pendientes pueden modificarse.");

        MetodoPago = metodoPago;
        Notas = notas;

        if (nuevosDetalles is not null && nuevosDetalles.Any())
        {
            _detalles.Clear();
            _detalles.AddRange(nuevosDetalles);
            CalcularTotal();
        }
    }
}
