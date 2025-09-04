using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _context;

    public FavoriteRepository(AppDbContext context)
    {
        _context  = context ;
    } 

    public async Task<List<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Favorites.Where(f => f.UserId == userId)
            .AsNoTracking()
            .ToListAsync() ;
    }
    
}