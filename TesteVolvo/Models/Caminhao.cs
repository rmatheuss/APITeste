using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVolvo.Models
{
    public class Caminhao
    {
        [Key]
        public int IdCaminhao { get; set; }

        [Required]
        public string? Nome { get; set; }
        
        [Required]
        [DisplayName("Ano de Fabricação")]
        public int AnoFabricacao { get; set; }

        [Required]
        [DisplayName("Ano do Modelo")]
        public int AnoModelo { get; set; }

        [DisplayName("Modelo")]
        [ForeignKey("IdModelo")]
        public int IdModelo { get; set; }

        [DisplayName("Modelo")]
        [NotMapped]
        public virtual Modelo Modelo { get; set; }
    }
}
