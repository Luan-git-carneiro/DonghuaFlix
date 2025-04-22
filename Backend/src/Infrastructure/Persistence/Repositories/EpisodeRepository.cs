using DonghuaFlix.src.Core.Application.Repositories;
using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.src.Infrastructure.Persistence.Repositories;

public class EpisodeRepository : IEpisodeRepository
{
    private readonly AppDbContext _context;

    public EpisodeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Episode> GetById(Guid id)
    {


        var episode = await _context.Episodes
            .Include(e => e.Video)
                .ThenInclude(v => v.Manifests)
                    .ThenInclude(m => m.Qualities)
            .FirstOrDefaultAsync(e => e.Id == id);
    
        if (episode == null)
        {
            return null;
        }
    
        return episode;
    }

    public async Task<IEnumerable<Episode>> GetAll(int pageSize = 20, int pageNumber = 1)
    {
        return await _context.Episodes
        .Include(e => e.Video)
            .ThenInclude(v => v.Manifests)
                .ThenInclude(m => m.Qualities)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public async Task<IEnumerable<Episode>> GetByDonghuaId(Guid donghuaId , int pageSize = 20, int pageNumber = 1)
    {
        return await _context.Episodes
        .Include(e => e.Video)
            .ThenInclude(v => v.Manifests)
                .ThenInclude(m => m.Qualities)
        .Where(e => e.DonghuaId == donghuaId)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public async Task AddEpisode(Episode episode)
    {
        await _context.Episodes.AddAsync(episode);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEpisode(Episode episode)
    {
        _context.Episodes.Update(episode);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEpisode(Guid id)
    {
        var episode = await GetById(id);
        _context.Episodes.Remove(episode);
        await _context.SaveChangesAsync();
    }




}