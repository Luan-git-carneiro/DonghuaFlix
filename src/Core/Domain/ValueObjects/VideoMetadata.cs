using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.ValueObjects
{
    public class VideoMetadata : ValueObject
    {
        public string Codec { get;  }
        public TimeSpan Duration { get; }
        public string Quality { get; }

        public VideoMetadata(string codec , TimeSpan duration, string quality)
        {
            Codec = codec;
            Duration = duration;
            Quality = quality;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Codec;
            yield return Duration;
            yield return Quality;
        }


    }
}