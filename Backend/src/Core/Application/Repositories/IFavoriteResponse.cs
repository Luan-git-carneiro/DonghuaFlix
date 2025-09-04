using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;


public interface IFavoriteRepository
{
    Task<List<Favorite>> GetByUserIdAsync(Guid userId);
    Task<List<Favorite>> GetByDonghuaIdAsync(Guid donghuaId);
    Task<bool> ExistsAsync(Guid userId, Guid donghuaId);
    Task AddAsync(Favorite favorite);
    Task RemoveAsync(Guid userId, Guid donghuaId);
}