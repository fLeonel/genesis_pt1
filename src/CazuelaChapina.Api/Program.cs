using DotNetEnv;
using CazuelaChapina.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CazuelaChapina.Application.Common.Interfaces;
using CazuelaChapina.Application.Features.Productos.Commands.CreateProducto;
using CazuelaChapina.Application.Features.Productos.Queries.GetProductos;
using CazuelaChapina.Application.Features.Productos.Queries.GetProductoById;
using CazuelaChapina.Application.Features.Productos.Commands.UpdateProducto;
using CazuelaChapina.Application.Features.Productos.Commands.DeleteProducto;
using CazuelaChapina.Application.Features.Combos.Commands.CreateCombo;
using CazuelaChapina.Application.Features.Combos.Queries.GetCombos;
using CazuelaChapina.Application.Features.Combos.Queries.GetComboById;
using CazuelaChapina.Application.Features.Combos.Commands.DeleteCombo;
using CazuelaChapina.Application.Features.Combos.Commands.UpdateCombo;
using CazuelaChapina.Application.Features.Clientes.Commands.CreateCliente;
using CazuelaChapina.Application.Features.Clientes.Commands.UpdateCliente;
using CazuelaChapina.Application.Features.Clientes.Commands.DeleteCliente;
using CazuelaChapina.Application.Features.Clientes.Queries.GetClientes;
using CazuelaChapina.Application.Features.Clientes.Queries.GetClienteById;
using CazuelaChapina.Application.Features.Ventas.Commands.CreateVenta;
using CazuelaChapina.Application.Features.Ventas.Commands.UpdateVenta;
using CazuelaChapina.Application.Features.Ventas.Commands.DeleteVenta;
using CazuelaChapina.Application.Features.Ventas.Queries.GetVentas;
using CazuelaChapina.Application.Features.Ventas.Queries.GetVentaById;
using CazuelaChapina.Application.Features.Categorias.Commands.CreateCategoria;
using CazuelaChapina.Application.Features.Categorias.Commands.UpdateCategoria;
using CazuelaChapina.Application.Features.Categorias.Commands.DeleteCategoria;
using CazuelaChapina.Application.Features.Categorias.Queries.GetCategorias;
using CazuelaChapina.Application.Features.Categorias.Queries.GetCategoriaById;
using CazuelaChapina.Application.Features.Recetas.Commands.CreateReceta;
using CazuelaChapina.Application.Features.Recetas.Commands.UpdateReceta;
using CazuelaChapina.Application.Features.Recetas.Commands.DeleteReceta;
using CazuelaChapina.Application.Features.Recetas.Queries.GetRecetas;
using CazuelaChapina.Application.Features.Recetas.Queries.GetRecetaById;
using Microsoft.OpenApi.Models;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=cazuela.db";
builder.Services.AddDbContext<CazuelaChapinaDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IAppDbContext, CazuelaChapinaDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cazuela Chapina API", Version = "v1" });
    c.TagActionsBy(api =>
    {
        var path = api.RelativePath ?? string.Empty;
        if (path.StartsWith("api/productos")) return new[] { "Productos" };
        if (path.StartsWith("api/combos")) return new[] { "Combos" };
        if (path.StartsWith("api/clientes")) return new[] { "Clientes" };
        if (path.StartsWith("api/ventas")) return new[] { "Ventas" };
        if (path.StartsWith("api/categorias")) return new[] { "Categorias" };
        if (path.StartsWith("api/recetas")) return new[] { "Recetas" };
        if (path.StartsWith("api/ai/insights")) return new[] { " GENESIS IA "};
        return new[] { "Otros" };
    });
    c.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddHttpClient<LlmService>();
builder.Services.AddScoped<SalesInsightsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

// Productos
app.MapPost("/api/productos", async (CreateProductoCommand cmd, CazuelaChapinaDbContext db) =>
{
    var handler = new CreateProductoHandler(db);
    var result = await handler.Handle(cmd);
    return Results.Created($"/api/productos/{result.Id}", result);
}).WithTags("Productos");

app.MapGet("/api/productos", async (IAppDbContext db) =>
{
    var handler = new GetProductosQuery(db);
    var productos = await handler.Handle();
    return Results.Ok(productos);
}).WithTags("Productos");

app.MapGet("/api/productos/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetProductoByIdQuery(db);
    var producto = await handler.Handle(id);
    return producto is not null ? Results.Ok(producto) : Results.NotFound();
}).WithTags("Productos");

app.MapPut("/api/productos/{id:guid}", async (Guid id, UpdateProductoCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateProductoHandler(db);
    var updated = await handler.Handle(cmd);
    return updated ? Results.NoContent() : Results.NotFound();
}).WithTags("Productos");

app.MapDelete("/api/productos/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteProductoHandler(db);
    var deleted = await handler.Handle(id);
    return deleted ? Results.NoContent() : Results.NotFound();
}).WithTags("Productos");

// Combos
app.MapPost("/api/combos", async (CreateComboCommand cmd, IAppDbContext db) =>
{
    var handler = new CreateComboHandler(db);
    var combo = await handler.Handle(cmd);
    return Results.Created($"/api/combos/{combo.Id}", combo);
}).WithTags("Combos");

app.MapGet("/api/combos", async (IAppDbContext db) =>
{
    var handler = new GetCombosQuery(db);
    var combos = await handler.Handle();
    return Results.Ok(combos);
}).WithTags("Combos");

app.MapGet("/api/combos/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetComboByIdQuery(db);
    var combo = await handler.Handle(id);
    return combo is not null ? Results.Ok(combo) : Results.NotFound();
}).WithTags("Combos");

app.MapDelete("/api/combos/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteComboHandler(db);
    var deleted = await handler.Handle(id);
    return deleted ? Results.NoContent() : Results.NotFound();
}).WithTags("Combos");

app.MapPut("/api/combos/{id:guid}", async (Guid id, UpdateComboCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateComboHandler(db);
    var updated = await handler.Handle(cmd);
    return updated ? Results.NoContent() : Results.NotFound();
}).WithTags("Combos");

// Clientes
app.MapPost("/api/clientes", async (CreateClienteCommand cmd, IAppDbContext db) =>
{
    var handler = new CreateClienteHandler(db);
    var cliente = await handler.Handle(cmd);
    return Results.Created($"/api/clientes/{cliente.Id}", cliente);
}).WithTags("Clientes");

app.MapGet("/api/clientes", async (IAppDbContext db) =>
{
    var handler = new GetClientesQuery(db);
    var clientes = await handler.Handle();
    return Results.Ok(clientes);
}).WithTags("Clientes");

app.MapGet("/api/clientes/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetClienteByIdQuery(db);
    var cliente = await handler.Handle(id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
}).WithTags("Clientes");

app.MapPut("/api/clientes/{id:guid}", async (Guid id, UpdateClienteCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateClienteHandler(db);
    var success = await handler.Handle(cmd);
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Clientes");

app.MapDelete("/api/clientes/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteClienteHandler(db);
    var success = await handler.Handle(new DeleteClienteCommand { Id = id });
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Clientes");

// Ventas
app.MapPost("/api/ventas", async (CreateVentaCommand cmd, IAppDbContext db) =>
{
    var handler = new CreateVentaHandler(db);
    var venta = await handler.Handle(cmd);
    return Results.Created($"/api/ventas/{venta.Id}", venta);
}).WithTags("Ventas");

app.MapGet("/api/ventas", async (IAppDbContext db) =>
{
    var handler = new GetVentasQuery(db);
    var ventas = await handler.Handle();
    return Results.Ok(ventas);
}).WithTags("Ventas");

app.MapGet("/api/ventas/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetVentaByIdQuery(db);
    var venta = await handler.Handle(id);
    return venta is not null ? Results.Ok(venta) : Results.NotFound();
}).WithTags("Ventas");

app.MapPut("/api/ventas/{id:guid}", async (Guid id, UpdateVentaCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateVentaHandler(db);
    var success = await handler.Handle(cmd);
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Ventas");

app.MapDelete("/api/ventas/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteVentaHandler(db);
    var success = await handler.Handle(new DeleteVentaCommand { Id = id });
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Ventas");

// Categorias
app.MapPost("/api/categorias", async (CreateCategoriaCommand cmd, IAppDbContext db) =>
{
    var handler = new CreateCategoriaHandler(db);
    var categoria = await handler.Handle(cmd);
    return Results.Created($"/api/categorias/{categoria.Id}", categoria);
}).WithTags("Categorias");

app.MapGet("/api/categorias", async (IAppDbContext db) =>
{
    var handler = new GetCategoriasQuery(db);
    var categorias = await handler.Handle();
    return Results.Ok(categorias);
}).WithTags("Categorias");

app.MapGet("/api/categorias/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetCategoriaByIdQuery(db);
    var categoria = await handler.Handle(id);
    return categoria is not null ? Results.Ok(categoria) : Results.NotFound();
}).WithTags("Categorias");

app.MapPut("/api/categorias/{id:guid}", async (Guid id, UpdateCategoriaCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateCategoriaHandler(db);
    var success = await handler.Handle(cmd);
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Categorias");

app.MapDelete("/api/categorias/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteCategoriaHandler(db);
    var success = await handler.Handle(new DeleteCategoriaCommand { Id = id });
    return success ? Results.NoContent() : Results.NotFound();
}).WithTags("Categorias");

// Recetas
app.MapPost("/api/recetas", async (CreateRecetaCommand cmd, IAppDbContext db) =>
{
    var handler = new CreateRecetaHandler(db);
    var receta = await handler.Handle(cmd);
    return Results.Created($"/api/recetas/{receta.Id}", receta);
}).WithTags("Recetas");

app.MapGet("/api/recetas", async (IAppDbContext db) =>
{
    var handler = new GetRecetasQuery(db);
    var recetas = await handler.Handle();
    return Results.Ok(recetas);
}).WithTags("Recetas");

app.MapGet("/api/recetas/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new GetRecetaByIdQuery(db);
    var receta = await handler.Handle(id);
    return receta is not null ? Results.Ok(receta) : Results.NotFound();
}).WithTags("Recetas");

app.MapPut("/api/recetas/{id:guid}", async (Guid id, UpdateRecetaCommand cmd, IAppDbContext db) =>
{
    cmd.Id = id;
    var handler = new UpdateRecetaHandler(db);
    var updated = await handler.Handle(cmd);
    return updated ? Results.NoContent() : Results.NotFound();
}).WithTags("Recetas");

app.MapDelete("/api/recetas/{id:guid}", async (Guid id, IAppDbContext db) =>
{
    var handler = new DeleteRecetaHandler(db);
    var deleted = await handler.Handle(id);
    return deleted ? Results.NoContent() : Results.NotFound();
}).WithTags("Recetas");

// GenesisIA =]:'3
app.MapGet("/api/ai/insights", async (SalesInsightsService ai) =>
{
    var insight = await ai.GenerateInsightAsync();
    return Results.Ok(new { recomendacion = insight });
})
.WithTags("AI")
.WithSummary("Análisis inteligente de ventas estacionales con DeepSeek R1");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
