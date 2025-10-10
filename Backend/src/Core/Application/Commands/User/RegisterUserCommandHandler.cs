
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
       
       
        if(userexists is null)
        {
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

        // A. Verifique se AMBOS são duplicados
        bool nameMatches = userexists.Name == request.Name;
        bool emailMatches = userexists.Email.Valor == request.Email; // Use .Valor, como em seus arquivos
    
        if (nameMatches && emailMatches)
        {
            return AuthenticationResult.UserAlreadyExists("Nome e E-mail já estão cadastrados, escolha outros!");
        }

        // B. Verifique se SOMENTE o Nome é duplicado
        if (nameMatches)
        {
            return AuthenticationResult.UserAlreadyExists("Nome já está cadastrado, escolha outro!");
        }

        // C. Verifique se SOMENTE o E-mail é duplicado
        if (emailMatches)
        {
            return AuthenticationResult.UserAlreadyExists("E-mail já está cadastrado, escolha outro!");
        }
        
        // Caso de segurança (se a busca OR achou algo, mas nenhum campo bateu.
        // Isso é raro com a busca OR, mas pode ser um caso de concorrência ou um erro
        // em que um campo opcional era null. Se o campo for NOT NULL, isso não ocorre).
        return AuthenticationResult.UserAlreadyExists("Conflito de registro detectado.");

    }

}