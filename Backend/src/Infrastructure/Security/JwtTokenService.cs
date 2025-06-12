using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using Microsoft.IdentityModel.Tokens;

namespace DonghuaFlix.Backend.src.Infrastructure.Security;

public class JwtTokenService : ITokenService
{
    
    private readonly IConfiguration _configuration;

    // Injetamos IConfiguration para ler o appsettings.json
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Guid userId, string email, UserRole role)
    {
        // 1. OBTER A CHAVE SECRETA
        // Lemos a chave do appsettings.json e a convertemos para um array de bytes.
        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT não está configurada em appsettings.json.");
        var key = Encoding.ASCII.GetBytes(jwtKey);

        // 2. CRIAR AS "CLAIMS"
        // Claims são as informações que queremos armazenar dentro do token.
        // O frontend ou outras APIs podem ler essas informações sem precisar ir ao banco de dados.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // ID do usuário - essencial para identificá-lo
            new Claim(ClaimTypes.Email, email),                      // Email do usuário
            new Claim(ClaimTypes.Role, role.ToString())              // Papel do usuário - essencial para autorização [Authorize(Roles="...")]
            // Você pode adicionar outras claims personalizadas se precisar
            // new Claim("nomeDaClaim", "valorDaClaim")
        };

        // 3. DESCREVER O TOKEN
        // SecurityTokenDescriptor é um objeto que descreve tudo sobre o token que queremos criar.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // Adicionamos as claims ao "assunto" do token

            // Define por quanto tempo o token será válido. É uma boa prática usar tempos curtos (ex: 1-8 horas).
            Expires = DateTime.UtcNow.AddHours(2),

            // Informações sobre quem emitiu (Issuer) e para quem se destina (Audience)
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            
            // Define as credenciais de assinatura, usando a chave secreta e o algoritmo de segurança.
            // Isso garante a autenticidade e integridade do token.
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        
        // 4. CRIAR E ESCREVER O TOKEN
        // Usamos um handler para criar o token com base na descrição e depois escrevê-lo como uma string.
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token); // Retorna a string final do JWT.
    }
}
