using System;

namespace CazuelaChapina.Domain.Entities
{
    public class MovimientoInventario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoId { get; set; }
        public double Cantidad { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty; // "Entrada", "Salida", "Merma"
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Motivo { get; set; } = string.Empty;

        public Producto Producto { get; set; } = null!;
    }
}
