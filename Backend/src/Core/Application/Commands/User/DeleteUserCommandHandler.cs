

using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user == null)
        {
            return AuthenticationResult.Failure("User not found.", "ERROR_NOT_FOUND");
        }

        //Verificar se usuario tem permissão
        if (user.Role != UserRole.Admin)
        {
            return AuthenticationResult.Failure("You do not have permission to delete this user.", "FORBIDDEN");
        }

        return AuthenticationResult.Success("User deleted successfully", user.Role);
    }
}