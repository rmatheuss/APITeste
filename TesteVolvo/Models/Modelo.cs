using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVolvo.Models
{
    public class Modelo
    {
        [Key]
        public int IdModelo { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [NotMapped]
        public virtual IList<Caminhao> Caminhoes { get; set; }
    }
}
