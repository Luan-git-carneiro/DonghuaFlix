using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using UserVo = DonghuaFlix.Backend.src.Core.Domain.Entities.User;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login
{
public class AuthenticationResult  : BaseResult<AuthenticationData>
{
    private AuthenticationResult(bool isSuccess, string? message ,  string? errorCode = null , AuthenticationData? data = null )
        : base(isSuccess, message,  errorCode , data)
    {

    }


    // CORRIGIDO: O método agora ACEITA o token e a role que ele precisa para trabalhar.
    public static AuthenticationResult Success(string token, DateTime? expiresAt ,UserRole role , UserDto user)
    {
        // 1. Com os parâmetros recebidos, criamos o objeto de dados.
        var authData = new AuthenticationData(token, role , expiresAt ,user);

        // 2. Agora chamamos o construtor privado, passando todos os valores necessários,
        // incluindo o objeto de dados que acabamos de criar.
        return new AuthenticationResult(true, "Autenticação realizada com sucesso", data: authData);
    }

    // Este método está correto porque ele não depende de dados externos.
    // Ele simplesmente cria um resultado de falha com valores fixos.
    public static AuthenticationResult UserAlreadyExists(string message) 
        => new(false,message, errorCode: "USER_ALREADY_EXISTS");

    public static AuthenticationResult Failure(string message, string errorCode )
        => new(false, message, errorCode);

}


}

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login
{
    
    public record AuthenticationData(string Token, UserRole Role, DateTime? expiresAt = null, UserDto? user = null) ;

}