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

    public async Task<Favorite?> GetFavoriteAsync(Guid userId , Guid donghuaId)
    {
        return await _context.Favorites.Where(f => f.UserId == userId && f.DonghuaId == donghuaId).AsNoTracking().FirstOrDefaultAsync() ;
    }

    public async Task<List<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Favorites.Where(f => f.UserId == userId)
            .AsNoTracking()
            .ToListAsync() ;
    }

    public async Task<List<Favorite>> GetByDonghuaIdAsync(Guid donghuaId)
    {
        return await _context.Favorites.Where(f => f.DonghuaId == donghuaId)
            .AsNoTracking()
            .ToListAsync() ;
    }

    public async Task<bool> ExistsAsync(Guid userId , Guid donghuaId )
    {
        return await _context.Favorites.AnyAsync(fa => fa.UserId == userId && fa.DonghuaId == donghuaId );
    }

    public async Task AddAsync(Favorite favorite)
    { 
        await _context.AddAsync(favorite);
        await _context.SaveChangesAsync() ;
    }

    public async Task RemoveAsync(Favorite favorite)
    {
        _context.Favorites.Remove(favorite) ;
        await _context.SaveChangesAsync() ;
    }
    
}