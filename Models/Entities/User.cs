namespace DonghuaFlix.Models.Entities
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType type { get; set; }
        public History history { get; set; }


    }
}

