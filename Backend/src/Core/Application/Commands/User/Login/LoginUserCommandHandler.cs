using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User.Login;


public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand , AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResult> Handle(LoginUserCommand request , CancellationToken  cancellationToken)
    {


        var user = await _userRepository.GetByEmailAsync(request.Email);

        if(user == null )
        {
            return AuthenticationResult.Failure("User not found.", "ERROR_NOT_FOUND");
        }

        bool isPasswordValid = user.Password.Validar(request.Password);

        if(!isPasswordValid)
        {
            return AuthenticationResult.Failure("Invalid password." , "UNAUTHORIZED" );
        }

        var token = _tokenService.GenerateToken(user.Id, user.Email.Valor, user.Role);

        var expirationDate = DateTime.UtcNow.AddHours(2);

        
        var userDto = new  UserDto(
            id: user.Id,
            name: user.Name,
            email: user.Email.Valor,
            createdAt: user.CreatedAt

        );
         
        return AuthenticationResult.Success(token, expirationDate , user.Role, userDto);

    }

}