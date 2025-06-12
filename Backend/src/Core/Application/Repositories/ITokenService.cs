using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email, UserRole role);
}