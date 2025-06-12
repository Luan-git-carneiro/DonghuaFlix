using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;

public class AuthenticationResult
{
    public bool IsSuccess { get;  }
    public string Token { get;  }
    public UserRole Role { get;  }
    public string Message { get;  }

    private AuthenticationResult(bool isSuccess, string token, UserRole role, string message)
    {
        IsSuccess = isSuccess;
        Token = token;
        Role = role;
        Message = message;
    }

    public static AuthenticationResult Success(string token, UserRole role) => new(true, token, role, string.Empty);
    public static AuthenticationResult Failure(string message) => new(false, string.Empty, UserRole.Regular, message);

}