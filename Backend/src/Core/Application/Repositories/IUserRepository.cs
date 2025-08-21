using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using  DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(string name, string email);
        Task<User> GetByEmailAsync(Email email);
        Task SaveChangesAsync();


        // =================== OPERAÇÕES DE CONSULTA AVANÇADA ===================
        /// <summary>
        /// Conta usuários aplicando filtros opcionais
        /// </summary>
        /// <param name="searchTerm">Termo para buscar em Nome ou Email</param>
        /// <param name="isActive">Filtro por status ativo/inativo</param>
        /// <returns>Total de usuários que atendem aos critérios</returns>
        Task<int> CountUsersAsync(string? searchTerm = null, bool? isActive = null);

        /// <summary>
        /// Busca usuários com paginação e filtros
        /// </summary>
        /// <param name="page">Número da página (base 1)</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <param name="searchTerm">Termo para buscar em Nome ou Email</param>
        /// <param name="isActive">Filtro por status ativo/inativo</param>
        /// <returns>Lista paginada de usuários</returns>
        Task<List<UserDto>> GetUsersPagedAsync(
            int page, 
            int pageSize, 
            string? searchTerm = null, 
            bool? isActive = null);

        // =================== OPERAÇÕES DE NEGÓCIO ESPECÍFICAS ===================
        Task<List<UserDto>> GetActiveUsersAsync();
        Task<List<UserDto>> GetUsersByRoleAsync(string role);
        Task<bool> IsEmailTakenAsync(string email, Guid? excludeUserId = null);

    }
