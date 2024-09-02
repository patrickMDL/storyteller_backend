using System.ComponentModel.DataAnnotations;

namespace StoryTeller.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Nick { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Senha { get; set; }
        [Required]
        public Boolean Is_admin { get; set; }
        public DateTime Ultimo_login { get; set; }
    }
}