namespace CazuelaChapina.Domain.Entities;

public class DashboardSnapshot : BaseEntity
{
    public DateTime Fecha { get; private set; } = DateTime.UtcNow;
    public decimal VentasDiarias { get; private set; }
    public decimal VentasMensuales { get; private set; }
    public decimal UtilidadTotal { get; private set; }
    public decimal DesperdicioMateriaPrima { get; private set; }

    private DashboardSnapshot() { }

    public DashboardSnapshot(decimal ventasDiarias, decimal ventasMensuales, decimal utilidadTotal, decimal desperdicio)
    {
        Fecha = DateTime.UtcNow;
        VentasDiarias = ventasDiarias;
        VentasMensuales = ventasMensuales;
        UtilidadTotal = utilidadTotal;
        DesperdicioMateriaPrima = desperdicio;
    }
}
