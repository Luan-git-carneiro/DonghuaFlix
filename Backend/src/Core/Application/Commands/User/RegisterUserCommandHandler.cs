
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using UserVo = DonghuaFlix.Backend.src.Core.Domain.Entities.User;
using EmailVo = DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Email;
using PasswordVo = DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Password;
using MediatR;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.User.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;

    public RegisterUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = new EmailVo(request.Email);
        var password = new PasswordVo(request.Password);
        var user = new UserVo(
            email: email,
            senha: password,
            nome: request.Name
        );

        // Verifica se o usuário já existe
        var (exists , userexists) = await userRepository.ExistsAsync(user.Name, user.Email.Valor);
       
       
        const bool nameExiste = 
        
        if(exists)
        {
             switch(userexists!.Name)
            {
                case request.Name:

                    AuthenticationResult.UserAlreadyExists();

                    break;
            }
        }

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();


        // Gera o token de autenticação
        var token = tokenService.GenerateToken(user.Id, user.Email.Valor, user.Role);

        var expirationDate = DateTime.UtcNow.AddHours(2);

        var userDto = new  UserDto(
            id: user.Id,
            name: user.Name,
            email: user.Email.Valor,
            createdAt: user.CreatedAt

        );

        // Cria o resultado de autenticação
       return AuthenticationResult.Success(token,expirationDate, user.Role , userDto );


    }

}