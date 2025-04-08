using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.src.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        if(userId == Guid.Empty)
        {
            throw new DomainValidationException(field: nameof(userId) , message: "Id do usuário é inválido.");
        }

       return  await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == userId);
    }

     public async Task<User> GetByEmailAsync(string email)
     {
         if(string.IsNullOrWhiteSpace(email))
         {
             throw new DomainValidationException(field: nameof(email) , message: "Email é inválido.");
         }

        return await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Email == email);
     }
    public async Task AddAsync(User user)
    {
        if(user is null)
        {
            throw new DomainValidationException(field: nameof(user) , message: "Usuário é nulo.");
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
        => await _context.Users.AnyAsync(u => u.Id == id);

    public async Task UpdateAsync(User user)
    {
        if(user is null)
        {
            throw new DomainValidationException(field: nameof(user) , message: "Usuário é nulo.");
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

}