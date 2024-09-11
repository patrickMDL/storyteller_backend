using System.ComponentModel.DataAnnotations;

namespace StoryTeller.Models
{
    public class ClassAbility
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Custo { get; set; }
        [Required]
        public String Descricao { get; set; }
        [Required]
        public Boolean Is_initial { get; set; }
    }
}