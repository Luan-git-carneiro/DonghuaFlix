
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User;


public class RegisterUserCommand : IRequest<AuthenticationResult>
{
    public string Email { get; }
    public string Password { get; }
    public string Name { get; }

    public string Role { get; } = "User"; // Default role, can be changed later


    public RegisterUserCommand(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
    }
}