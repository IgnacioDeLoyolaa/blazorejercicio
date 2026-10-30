using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazor.Models
{
    public class PreguntasGeneradas
    {
        [Key]
        public int Id { get; set; }

        public int IdDuda { get; set; }

        [ForeignKey("IdDuda")]
        public Dudas duda { get; set; } = null!;

        public string? texto { get; set; }
        public string? a { get; set; }
        public string? b { get; set; }
        public string? c { get; set; }
        public string? d { get; set; }
        public string? correcta { get; set; }
        public string? bloque { get; set; }
        public string? parte { get; set; }
        public int? tema { get; set; }
    }
}
