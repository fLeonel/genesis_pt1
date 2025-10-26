namespace CazuelaChapina.Domain.Entities;

using System.Text.Json.Serialization;

public class Cliente : BaseEntity
{
    public string ClienteCodigo { get; private set; } = null!;
    public string Nombre { get; private set; } = null!;
    public string? Telefono { get; private set; }
    public string? Correo { get; private set; }
    public string? Direccion { get; private set; }
    public string Nit { get; private set; } = "CF";

    [JsonIgnore]
    private readonly List<Venta> _ventas = new();
    public IReadOnlyCollection<Venta> Ventas => _ventas.AsReadOnly();

    private Cliente() { }

    public Cliente(
        string clienteCodigo,
        string nombre,
        string? direccion = null,
        string? nit = "CF",
        string? telefono = null,
        string? correo = null)
    {
        ClienteCodigo = clienteCodigo;
        Nombre = nombre;
        Nit = string.IsNullOrWhiteSpace(nit) ? "CF" : nit;
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
        string? direccion = null,
        string? nit = null)
    {
        if (!string.IsNullOrWhiteSpace(nombre))
            Nombre = nombre;

        if (!string.IsNullOrWhiteSpace(telefono))
            Telefono = telefono;

        if (!string.IsNullOrWhiteSpace(correo))
            Correo = correo;

        if (!string.IsNullOrWhiteSpace(direccion))
            Direccion = direccion;

        if (!string.IsNullOrWhiteSpace(nit))
            Nit = nit;
    }
}
