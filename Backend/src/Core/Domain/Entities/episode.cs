using DonghuaFlix.Backend.src.Core.Domain.Abstractions;

namespace DonghuaFlix.Backend.src.Core.Domain.Entities;

public class Episode : Entity
{
    public string Title { get; private set;}
    public string Description { get; private set;}
    public float Score { get; private set;}
    public int Number { get; private set; }
    public TimeSpan Duration { get; private set; }
    public VideoAsset Video { get; private set; }
    public string Thumbnail { get; private set;}
    public Guid DonghuaId { get; private set; }

    protected Episode() { }
    public Episode(string title , string description , float score ,int number, TimeSpan duration, VideoAsset video, string thumbnail , Guid donghuaId)
    {
        Title = title;
        Description = description;
        Score = score ;
        Number = number;
        Duration = duration;
        Video = video;
        Thumbnail = thumbnail;
        DonghuaId = donghuaId;
    }

    public void UpdateVideo(VideoAsset video)
    {
        Video = video;
    //    AddDomainEvent(new VideoEpisodioAtualizadoEvent(Id));
    }



}