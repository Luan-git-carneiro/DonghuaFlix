using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User.Login;

public class LoginUserCommand : IRequest<AuthenticationResult>
{
    public string Email { get; set;  }
    public string Password { get; set; }

    public LoginUserCommand(string email, string password) 
    {
        Email = email;
        Password = password;
    }


}