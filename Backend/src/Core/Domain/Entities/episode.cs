using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Episode : Entity
{
    public int Number { get; private set; }
    public TimeSpan Duration { get; private set; }
    public VideoAsset Video { get; private set; }
    public Guid DonghuaId { get; private set; }

 
    public Episode(int number, TimeSpan duration, VideoAsset video, Guid donghuaId)
    {
        Number = number;
        Duration = duration;
        Video = video;
        DonghuaId = donghuaId;
    }

    public void UpdateVideo(VideoAsset video)
    {
        Video = video;
    //    AddDomainEvent(new VideoEpisodioAtualizadoEvent(Id));
    }



}