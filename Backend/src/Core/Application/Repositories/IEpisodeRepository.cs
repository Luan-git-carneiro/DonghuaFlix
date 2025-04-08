using DonghuaFlix.src.Core.Domain.Entities;

namespace DonghuaFlix.src.Core.Application.Repositories;

public class IEpisodeRepository
{
    Task<Episode> GetById(Guid id);
    Task<IEnumerable<Episode>> GetAll();
    Task<IEnumerable<Episode>> GetByDonghuaId(Guid donghuaId);
    Task AddEpisode(Episode episode);
    Task UpdateEpisode(Episode episode);
    Task DeleteEpisode(Guid id);
}