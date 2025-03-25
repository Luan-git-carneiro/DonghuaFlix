using DonghuaFlix.src.Core.Domain.Entities;

namespace DonghuaFlix.src.Core.Aplication.Repositories;

public class IDonghuaRepository
{
    Task AddAsync(Donghua donghua);
    Task<Donghua> GetByIdAsync(Guid id);
    Task<List<Donghua>> GetAllAsync();
    Task UpdateAsync(Donghua donghua);
    Task DeleteAsync(Donghua donghua);
}