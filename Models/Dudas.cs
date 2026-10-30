using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazor.Models
{
    public class Dudas
    {
        [Key]
        public int Id { get; set; }

        public int IdPregunta { get; set; }

        [ForeignKey("IdPregunta")]
        public Preguntas IdPreguntas { get; set; } = null!;

        public bool? A_en_Dudas { get; set; }
        public bool? B_en_Dudas { get; set; }
        public bool? C_en_Dudas { get; set; }
        public bool? D_en_Dudas { get; set; }

        public string Marcada { get; set; } = string.Empty;
        public bool Acertada { get; set; } = false;
    }
}
