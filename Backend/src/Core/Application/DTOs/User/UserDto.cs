namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDto(Guid id , string name , string email, DateTime createdAt)
    {
        Id = id;
        Name = name ;
        Email = email ;
        CreatedAt = createdAt;
    }
        // Adicione este construtor vazio
    public UserDto() { }

}