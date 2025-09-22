

using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
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


        var userDto = new  UserDto(
            id: user.Id,
            name: user.Name,
            email: user.Email.Valor,
            createdAt: user.CreatedAt

        );

        return AuthenticationResult.Success("User deleted successfully", user.Role , userDto);
    }
}