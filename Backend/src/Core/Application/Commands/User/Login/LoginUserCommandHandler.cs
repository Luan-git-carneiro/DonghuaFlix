using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
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
        if(string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return AuthenticationResult.Failure("Email and password cannot be empty.");
        }

        Email email;

        try
        {
            email = new Email(request.Email);

        } catch (DomainValidationException ex)
        {
            return AuthenticationResult.Failure(ex.Message);
        }

        var user = await _userRepository.GetByEmailAsync(email);

        if(user == null )
        {
            return AuthenticationResult.Failure("User not found.");
        }

        bool isPasswordValid = user.Password.Validar(request.Password);

        if(!isPasswordValid)
        {
            return AuthenticationResult.Failure("Invalid password.");
        }

        var token = _tokenService.GenerateToken(user.Id, user.Email.Valor, user.Role);
         
        return AuthenticationResult.Success(token, user.Role);

    }

}