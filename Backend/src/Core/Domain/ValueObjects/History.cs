using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;

namespace DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

public class History : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid EpisodeId { get; private set; }
    public DateTime DateVisualization { get; private set; }
   

    //construtor privado para o EF
    public History( Guid userId ,Guid episodeId, DateTime dateVisualization)
    {
        if(episodeId == Guid.Empty)
        {
            throw new DomainValidationException(field: nameof(episodeId) , message: "Id do episódio é inválido.");
        }
        UserId = userId;
        EpisodeId = episodeId;
        DateVisualization = dateVisualization;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return EpisodeId;
        yield return DateVisualization;
    }
}