using DonghuaFlix.src.Core.Domain.Entities;

namespace DonghuaFlix.src.Core.Aplication.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(Guid id);
        
    }
}