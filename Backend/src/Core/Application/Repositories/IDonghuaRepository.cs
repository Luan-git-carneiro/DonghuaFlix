using System.Linq.Expressions;
using DonghuaFlix.Backend.src.Core.Domain.Entities;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

public interface IDonghuaRepository
{
    
    Task<Donghua?> GetByIdAsync(Guid id);
    Task AddAsync(Donghua donghua);
    Task<List<Donghua>> GetAllAsync( int limit);
    Task UpdateAsync(Donghua donghua);
    Task DeleteAsync(Donghua donghua);
    Task<bool> AnyAsync(Expression<Func<Donghua, bool>> predicate);
}