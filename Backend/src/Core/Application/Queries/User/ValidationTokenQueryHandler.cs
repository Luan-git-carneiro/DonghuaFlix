namespace DonghuaFlix.Backend.src.Core.Application.Queries.User;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using MediatR;
using System.Text;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;

public class ValidationTokenQueryHandler : IRequestHandler<ValidationTokenQuery, ValidationResult>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public ValidationTokenQueryHandler(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Handle(ValidationTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validar token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Não tolerância para expiração
                };

                // Validar token
                var principal = tokenHandler.ValidateToken(request.Token, validationParameters, out _);
                
                // 2. Extrair claims
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var name = principal.FindFirst(ClaimTypes.Name)?.Value;
                var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
                {
                    return ValidationResult.Failure("Token inválido: ID de usuário não encontrado");
                }

                // 3. Verificar se usuário ainda existe no banco
                var user = await _userRepository.GetByIdAsync(userGuid);
                if (user == null )
                {
                    return ValidationResult.Failure("Usuário não encontrado ou inativo");
                }

                // 4. Parse do role
                if (!Enum.TryParse<UserRole>(roleClaim, out var role))
                {
                    return ValidationResult.Failure("Role inválida no token");
                }

                // 5. Retornar dados do usuário
                var userDto = new UserValidationDto
                {
                    Id = userId,
                    Email = email ?? string.Empty,
                    Name = name ?? string.Empty,
                    Role = role
                };

                return ValidationResult.Success(userDto);
            }
            catch (SecurityTokenExpiredException)
            {
                return ValidationResult.Failure("Token expirado");
            }
            catch (Exception ex)
            {
                return ValidationResult.Failure($"Erro na validação: {ex.Message}");
            }
        }
    }