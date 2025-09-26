namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public UserValidationDto? User { get; set; }
        public string? Error { get; set; }
        
        public static ValidationResult Success(UserValidationDto user) 
            => new() { IsValid = true, User = user };
        
        public static ValidationResult Failure(string error) 
            => new() { IsValid = false, Error = error };
    }