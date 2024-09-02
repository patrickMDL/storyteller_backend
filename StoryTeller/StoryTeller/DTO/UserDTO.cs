namespace StoryTeller.DTO
{
    public class UserDTO()
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Nick { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public Boolean Is_admin { get; set; }
        public DateTime Ultimo_login { get; set; }
    }
}