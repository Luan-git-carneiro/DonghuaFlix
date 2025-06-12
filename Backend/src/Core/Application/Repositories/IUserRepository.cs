using  DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(Guid id);
        Task<User> GetByEmailAsync(Email email);
        
    }
