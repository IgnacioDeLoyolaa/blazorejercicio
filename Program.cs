using blazor.Components;
using blazor.Data;
using blazor.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- 1. CONFIGURACIÓN DE SERVICIOS ----------

// Blazor: registra los componentes .razor y el modo interactivo en servidor
// (necesario para que funcionen los @onclick de la página del test).
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Lee la cadena de conexión de appsettings.json.
// Si no existe, detiene la aplicación con un mensaje claro en vez de fallar más tarde.
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Registra el DbContext para que se pueda pedir como parámetro en los endpoints.
// UseMySql indica a EF Core que use MySQL (Pomelo).
// ServerVersion.AutoDetect se conecta una vez al arrancar para saber qué versión
// de MySQL tienes (8.4) y generar el SQL adecuado. Por eso MySQL tiene que estar encendido.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Registra la generación del documento OpenAPI (descripción de la API).
builder.Services.AddOpenApi();

var app = builder.Build();

// ---------- 2. PIPELINE HTTP ----------

// Solo en desarrollo: expone /openapi/v1.json con la descripción de todos los endpoints.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirige las peticiones http:// a https://
app.UseHttpsRedirection();

// Protección contra peticiones falsificadas; Blazor la exige.
app.UseAntiforgery();

// Sirve los archivos de wwwroot (app.css, bootstrap...).
app.MapStaticAssets();

// ---------- 3. ENDPOINTS ----------

// GET /preguntas
// Devuelve todas las preguntas en JSON.
// ASP.NET Core crea el ApplicationDbContext para cada petición y lo pasa como parámetro.
app.MapGet("/preguntas", async (ApplicationDbContext context) =>
{
    List<Preguntas> preguntas = await context.Preguntas.ToListAsync();
    return Results.Ok(preguntas);
})
.WithName("GetPreguntas");

// GET /get-random-question
// Devuelve una pregunta al azar, sin cargar toda la tabla en memoria:
//   1) cuenta cuántas preguntas hay (SELECT COUNT(*)),
//   2) elige una posición al azar,
//   3) pide solo esa fila (ORDER BY Id LIMIT 1 OFFSET n).
app.MapGet("/get-random-question", async (ApplicationDbContext context) =>
{
    int total = await context.Preguntas.CountAsync();

    // Si la tabla está vacía, devuelve 404 en vez de lanzar una excepción.
    if (total == 0)
        return Results.NotFound("No hay preguntas en la base de datos.");

    int posicion = Random.Shared.Next(total);   // número entre 0 y total-1

    Preguntas pregunta = await context.Preguntas
        .OrderBy(p => p.Id)    // Skip necesita un orden fijo para ser fiable
        .Skip(posicion)
        .FirstAsync();

    return Results.Ok(pregunta);
})
.WithName("GetRandomQuestion");

// Páginas Blazor: App.razor es la raíz y desde ahí el Router carga
// Home, Test, etc. según la URL.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
