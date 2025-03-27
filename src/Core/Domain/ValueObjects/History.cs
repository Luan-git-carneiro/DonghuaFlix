using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.ValueObjects;

public class History : ValueObject
{

    public Guid IdEpisode { get; private set; }
    public DateTime DateCreat { get; private set; }
   

    //construtor privado para o EF
    public History( Guid idEpisode, DateTime dateCreat)
    {
        IdEpisode = idEpisode;
        DateCreat = dateCreat;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IdEpisode;
        yield return DateCreat;
    }
}