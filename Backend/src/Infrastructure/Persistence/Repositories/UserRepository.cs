using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;

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

     public async Task<User> GetByEmailAsync(Email email)
     {
         if(string.IsNullOrWhiteSpace(email.Valor))
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
        
        // A responsabilidade de chamar SaveChangesAsync agora é do handler que usar este método.
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