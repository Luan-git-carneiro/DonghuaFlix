using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;
using DonghuaFlix.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.src.Core.Domain.Entities;

    public class VideoManifest : ValueObject
    {
        public Guid VideoAssetId { get; private set; }
        public string CodecPrincipal { get; private set; } // h264, h265, av1
        public string Protocolo { get; private set; }  // HTTP Live Streaming HLS , MPEG-DASH ETC   
        private List<VideoQualityProfile> _qualities = new();
        public IReadOnlyList<VideoQualityProfile> Qualities => _qualities.AsReadOnly();


        //construtor privado para o EF
        public VideoManifest( Guid videoAssetId , string protocolo, string codecPrincipal)
        {
            VideoAssetId = videoAssetId;
            Protocolo = protocolo;
            CodecPrincipal = codecPrincipal;
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
    
    
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Protocolo;
            yield return CodecPrincipal;
            foreach (var quality in _qualities) yield return quality;
            yield return VideoAssetId;

        }
    
    }
