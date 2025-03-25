using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;
using DonghuaFlix.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.src.Core.Domain.Entities;
public class VideoAsset : Entity
{
    private List<VideoManifest> _manifests = new();
    public VideoMetadata Metadata { get; private set; }

    public IReadOnlyList<VideoManifest> Manifests => _manifests.AsReadOnly();

    public VideoAsset(VideoMetadata metadata)
    {
        Metadata = metadata;
    }

    public void AddManifest(VideoManifest manifest)
    {
        // Validação: 1 manifesto por codec
        if (_manifests.Any(m => m.Qualities.Any(q => q.Codec == manifest.Qualities.First().Codec)))
            throw new BusinessRulesException( rulesName: "DUPLICATE" , message: "Codec já existe em outro manifesto");
            
        _manifests.Add(manifest);
    }
}