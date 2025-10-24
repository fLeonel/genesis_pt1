namespace CazuelaChapina.Domain.Entities;

public class Cliente : BaseEntity
{
    public string ClienteCodigo { get; private set; } = null!;
    public string Nombre { get; private set; } = null!;
    public string? Telefono { get; private set; }
    public string? Correo { get; private set; }
    public string? Direccion { get; private set; }

    private readonly List<Venta> _ventas = new();
    public IReadOnlyCollection<Venta> Ventas => _ventas.AsReadOnly();

    private Cliente() { }

    public Cliente(
        string clienteCodigo,
        string nombre,
        string? telefono = null,
        string? correo = null,
        string? direccion = null)
    {
        ClienteCodigo = clienteCodigo;
        Nombre = nombre;
        Telefono = telefono;
        Correo = correo;
        Direccion = direccion;
    }

    public void RegistrarVenta(Venta venta)
    {
        _ventas.Add(venta);
    }

    public void UpdateCliente(
        string? nombre = null,
        string? telefono = null,
        string? correo = null,
        string? direccion = null)
    {
        if (!string.IsNullOrWhiteSpace(nombre))
            Nombre = nombre;

        if (!string.IsNullOrWhiteSpace(telefono))
            Telefono = telefono;

        if (!string.IsNullOrWhiteSpace(correo))
            Correo = correo;

        if (!string.IsNullOrWhiteSpace(direccion))
            Direccion = direccion;
    }
}
