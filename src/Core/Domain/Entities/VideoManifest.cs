using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.Entities
{
    public class VideoManifest : Entity
    {
        public Guid VideoAssetId { get; private set; }
        private List<VideoQualityProfile> _qualities = new();
        public IReadOnlyList<VideoQualityProfile> Qualities => _qualities.AsReadOnly();


        //construtor privado para o EF
        private VideoManifest(Guid videoAssetId)
        {
            VideoAssetId = videoAssetId;
        }

        public void AddQualityProfile(VideoQualityProfile profile)
        {
            // Garantir codecs compatíveis
            if (!IsCodecSupported(profile.Codec))
            {
                throw new DomainException($"Codec {profile.Codec} não suportado");
   
            } 
            _qualities.Add(profile);
        }

        private bool IsCodecSupported(string codec)
            => new[] { "h264", "h265", "av1" }.Contains(codec);
    }
}