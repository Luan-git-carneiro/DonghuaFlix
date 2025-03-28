using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.ValueObjects;

public sealed class VideoQualityProfile : ValueObject
{
    public string Quality { get; } // 360p, 720p, etc
    public int Bitrate { get; } // Em kbps
    public string Codec { get; }
    public string Path { get; } // Caminho para playlist/segmentos

    public VideoQualityProfile(string qualidade, int bitrate, string codec, string caminho)
    {
        Quality = qualidade;
        Bitrate = bitrate;
        Codec = codec;
        Path = caminho;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quality;
        yield return Bitrate;
        yield return Codec;
        yield return Path;
    }
}