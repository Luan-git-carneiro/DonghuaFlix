using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Episode : Entity
{
    public int Number { get; private set; }
    public TimeSpan Duration { get; private set; }
    public VideoAsset Video { get; private set; }
    public Guid IdDonghua { get; private set; }

 
    public Episode(int number, TimeSpan duration, VideoAsset video, Guid idDonghua)
    {
        Number = number;
        Duration = duration;
        Video = video;
        IdDonghua = idDonghua;
    }

    public void UpdateVideo(VideoAsset video)
    {
        Video = video;
    //    AddDomainEvent(new VideoEpisodioAtualizadoEvent(Id));
    }



}