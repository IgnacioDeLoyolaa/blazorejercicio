// Necesario: aquí están DbContext, DbSet, DbContextOptions y ModelBuilder.
// No borrar aunque el editor lo marque como "innecesario".
using Microsoft.EntityFrameworkCore;
using blazor.Models;

namespace blazor.Data;

// El DbContext representa la conexión con la base de datos.
// Cada DbSet<T> equivale a una tabla: consultar el DbSet es consultar la tabla.
public class ApplicationDbContext : DbContext
{
    // Las opciones (cadena de conexión, proveedor MySQL) llegan desde Program.cs
    // mediante inyección de dependencias con AddDbContext.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tabla "Preguntas"
    public DbSet<Preguntas> Preguntas { get; set; } = null!;

    // Tabla "Dudas". Cada duda apunta a una pregunta (IdPregunta).
    public DbSet<Dudas> Dudas { get; set; } = null!;

    // Tabla "PreguntasGeneradas". Cada una apunta a una duda (IdDuda).
    public DbSet<PreguntasGeneradas> PreguntasGeneradas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
