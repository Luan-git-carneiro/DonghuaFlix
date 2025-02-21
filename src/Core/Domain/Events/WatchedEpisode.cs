namespace DonghuaFlix.src.Core.Domain.Events;

public class WatchedEpisode 
{
    public Guid IdWatchedEpisode { get; private set; }
    public Guid IdUser { get; private set; }
    public Guid IdEpisode { get; private set; }
    public DateTime WatchedDate { get; private set; }

    public WatchedEpisode(Guid idUser, Guid idEpisode)
    {
        IdUser = idUser;
        IdEpisode = idEpisode;
        IdWatchedEpisode = Guid.NewGuid();
        WatchedDate = DateTime.UtcNow;
    }
    
}