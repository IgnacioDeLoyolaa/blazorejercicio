using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazor.Models
{
    // Clase modelo: cada objeto Preguntas es una fila de la tabla.
    // [Table] indica el nombre exacto de la tabla en MySQL ("preguntas", en
    // minúscula, tal como la crea preguntas.sql). Sin él, EF Core buscaría
    // una tabla llamada como el DbSet ("Preguntas").
    // Las propiedades se llaman igual que las columnas para que EF las
    // relacione solas. Solo se mapean las columnas que usa la aplicación.
    [Table("preguntas")]
    public class Preguntas
    {
        [Key]
        public int Id { get; set; }
        public string? texto { get; set; }
        public string? a { get; set; }
        public string? b { get; set; }
        public string? c { get; set; }
        public string? d { get; set; }
        public string? correcta { get; set; }
        public int? anyo { get; set; }
        public string? bloque { get; set; }
        public string? parte { get; set; }
        // int? (admite null) porque en la tabla la columna es "DEFAULT NULL";
        // con int normal EF lanzaría una excepción al leer una fila con NULL.
        public int? n_pregunta { get; set; }
        public int? tema { get; set; }
    }
}
