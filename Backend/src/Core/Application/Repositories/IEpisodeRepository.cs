using  DonghuaFlix.Backend.src.Core.Domain.Entities;

namespace DonghuaFlix.Backend.src.Core.Application.Repositories;

public interface IEpisodeRepository 
{
    Task<Episode> GetById(Guid id);
    Task<IEnumerable<Episode>> GetAll(int pageSize , int pageNumber );
    Task<IEnumerable<Episode>> GetByDonghuaId(Guid donghuaId , int pageSize  , int pageNumber );
    Task AddEpisode(Episode episode);
    Task UpdateEpisode(Episode episode);
    Task DeleteEpisode(Guid id);
}