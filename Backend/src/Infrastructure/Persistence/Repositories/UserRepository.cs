using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
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

       return  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

     public async Task<User> GetByEmailAsync(Email email)
     {
         if(string.IsNullOrWhiteSpace(email.Valor))
         {
             throw new DomainValidationException(field: nameof(email) , message: "Email é inválido.");
         }

        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Valor == email);
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

    public async Task<bool> ExistsAsync(string name , string email )
        => await _context.Users.AnyAsync(u => u.Name == name && u.Email.Valor == email);

    public async Task UpdateAsync(User user)
    {
        if(user is null)
        {
            throw new DomainValidationException(field: nameof(user) , message: "Usuário é nulo.");
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // =================== OPERAÇÕES DE CONSULTA AVANÇADA ===================
    
    /// <summary>
    /// 🎯 MÉTODO PRINCIPAL: Conta usuários com filtros
    /// Este método é fundamental para paginação inteligente
    /// </summary>
    public async Task<int> CountUsersAsync(string? searchTerm = null)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();

        // Aplicar filtros usando método reutilizável
        query = ApplyFilters(query, searchTerm);

        return await query.CountAsync();
    }

    /// <summary>
    /// 🎯 MÉTODO PRINCIPAL: Busca paginada com filtros
    /// Implementa toda a lógica de paginação de forma otimizada
    /// </summary>
    public async Task<List<UserDto>> GetUsersPagedAsync(int page, int pageSize, string? searchTerm = null)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();
        
        //Aplicar Filtros
        query = ApplyFilters(query, searchTerm);

        //Aplicar ordenação consistente e Paginação
        var users = await query
        .OrderBy(u => u.Name) // Ordenar por Nome
        .ThenBy(u => u.Id) // Garantir consistência na ordenação
        .Skip((page - 1) * pageSize) // Pular os itens das páginas anteriores
        .Take(pageSize) // Pegar apenas o tamanho da página
       .Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email.Valor,
            CreatedAt = u.CreatedAt
        })
        .ToListAsync();

        return users;
    }

    // =================== OPERAÇÕES DE NEGÓCIO ESPECÍFICAS ===================
    public async Task<List<UserDto>> GetActiveUsersAsync()
    {
        var users = await _context.Users
            .AsNoTracking()
            .Where(u => u.Status == AccountStatus.Active)
            .OrderBy(u => u.Name)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email.Valor,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        return users;
    }

    public async Task<List<UserDto>> GetUsersByRoleAsync(string role)
    {
        var users = await _context.Users
            .AsNoTracking()
            .Where(u => u.Role.ToString() == role)
            .OrderBy(u => u.Name)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email.Valor,
                CreatedAt = u.CreatedAt,
            })
            .ToListAsync();

        return users;
    }

    public async Task<bool> IsEmailTakenAsync(string email, Guid? excludeUserId = null)
    {
        var query = _context.Users.AsNoTracking()
            .Where(u => u.Email.Valor.ToLower() == email.ToLower());

        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }

        return await query.AnyAsync();
    }

    // =================== MÉTODOS AUXILIARES PRIVADOS ===================
    
    /// <summary>
    /// 🔧 MÉTODO REUTILIZÁVEL: Aplica filtros comuns
    /// Centraliza a lógica de filtros para evitar duplicação
    /// </summary>
    private static IQueryable<User> ApplyFilters(
        IQueryable<User> query, 
        string? searchTerm)
    {
        // Filtro de busca por texto (Nome ou Email)
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            query = query.Where(u => 
                u.Name.ToLower().Contains(search) || 
                u.Email.Valor.ToLower().Contains(search));
        }

        return query;
    }

    /// <summary>
    /// 🔄 MAPPER: Converte Entity para DTO
    /// Centraliza a conversão para manter consistência
    /// </summary>
    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Valor,
            CreatedAt = user.CreatedAt,
        };
    }


}