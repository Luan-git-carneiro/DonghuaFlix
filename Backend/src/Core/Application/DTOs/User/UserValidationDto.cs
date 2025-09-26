using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;

 public class UserValidationDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }