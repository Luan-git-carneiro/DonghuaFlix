using  DonghuaFlix.Backend.src.Core.Domain.Entities;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(Guid id);
        
    }
