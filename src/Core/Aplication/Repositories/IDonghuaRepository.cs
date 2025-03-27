using DonghuaFlix.src.Core.Domain.Entities;

namespace DonghuaFlix.src.Core.Aplication.Repositories;

public interface IDonghuaRepository
{
    
    Task<Donghua?> GetByIdAsync(Guid id);
    Task AddAsync(Donghua donghua);
    Task<List<Donghua>> GetAllAsync( int limit);
    Task UpdateAsync(Donghua donghua);
    Task DeleteAsync(Donghua donghua);
}