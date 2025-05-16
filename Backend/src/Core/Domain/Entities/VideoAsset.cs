using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

namespace DonghuaFlix.Backend.src.Core.Domain.Entities;
public class VideoAsset : Entity
{
    private List<VideoManifest> _manifests = new();
    public IReadOnlyList<VideoManifest> Manifests => _manifests.AsReadOnly();
 
    // Metadados básicos
    public DateTime DataUpload { get; private set; } = DateTime.UtcNow;
    public string CaminhoStorage { get; private set; } // Caminho no S3/Blob Storage


    public VideoAsset(string caminhoStorage)
    {
        CaminhoStorage = caminhoStorage;
    }

    public void AddManifest(VideoManifest manifest)
    {
        // Validação: 1 manifesto por codec
        if (_manifests.Any(m => m.Qualities.Any(q => q.Codec == manifest.Qualities.First().Codec)))
            throw new BusinessRulesException( rulesName: "DUPLICATE" , message: "Codec já existe em outro manifesto");
            
        _manifests.Add(manifest);
    }
}