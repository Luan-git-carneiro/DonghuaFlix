

using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User.DeleteUser;

public class DeleteUserCommand : IRequest<AuthenticationResult>
{
    public Guid UserId { get; }

    public DeleteUserCommand(Guid userId )
    {
        UserId = userId;
    }
}